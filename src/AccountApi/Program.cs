
using AccountApi.Application.Validators;
using AccountApi.Infrastructure.DependencyResolver.AutofacHelper;
using AccountApi.Infrastructure.Helpers.JWT;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Helpers.Security.Encryption;
using System.Text.Json.Serialization;
using FluentValidation;
using Shared.Helpers.Mailing;
using MassTransit;
using AccountApi.Infrastructure.Helpers.Consumer;
using Shared.Constant;
using Shared.Events;
using AccountApi.WebApi.Configuration;
using Autofac.Core;
using System.Security.Claims;

namespace AccountApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigurationManager configurationManager = builder.Configuration;

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
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
            var tokenOptions = configurationManager.GetSection("TokenOptions").Get<Shared.Helpers.Security.Security.TokenOptions>();
            TokenValidate.AccountOptions = tokenOptions;

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddRabbitMqWithDLX(builder.Configuration);
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
                                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                                    RoleClaimType = ClaimTypes.Role
                                };
                            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
