
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Shared.Constant;
using Shared.Helpers.Security.Encryption;
using Shared.Helpers.Security.Security;
using SupplierAPI.Constants;
using SupplierAPI.Infrastructure.DependencyResolver.AutofacHelper;
using SupplierAPI.Infrastructure.Consumer;
using SupplierAPI.Infrastructure.Background.Quartz;
using SupplierAPI.WebApi.Extensions;

namespace SupplierAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configurationManager = builder.Configuration;

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            builder.Services.AddQuartz(q =>
            {
                var jobKey = new JobKey("SendProcess");
                q.AddJob<LookWarning>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("SendProcess-trigger")
                    .WithCronSchedule("0 * * ? * *")
                );
            });
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

            builder.Services.AddSession();

            var tokenOptions = configurationManager.GetSection("TokenOptions").Get<TokenOptions>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidIssuer = tokenOptions.Issuer,
                                    ValidAudience = tokenOptions.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                                };
                            });
           
            var rabbitOption = configurationManager.GetSection("RabbitOptions").Get<RabbitOptions>();

            builder.Services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<BillConsumer>();

                configurator.UsingRabbitMq((context, _configurator) =>
                {
                    _configurator.Host(rabbitOption.Url, h =>
                    {
                        h.Username(rabbitOption.User);
                        h.Password(rabbitOption.Password);
                    });

                    _configurator.ReceiveEndpoint(RabbitMQSettings.Bill_OrderCreatedEventQueue, e =>
                    e.ConfigureConsumer<BillConsumer>(context));
                });
            });

            var app = builder.Build();
            ServiceTool.ContainerServiceCreate(builder.Services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
