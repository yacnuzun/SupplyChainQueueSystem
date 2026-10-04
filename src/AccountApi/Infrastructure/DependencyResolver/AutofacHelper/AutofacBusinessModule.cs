using AccountApi.Application.Services.Implementations;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Application.Validators;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Helpers.JWT;
using AccountApi.Infrastructure.Helpers.Mail.Implemantations;
using AccountApi.Infrastructure.Repositories.Implemantations;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Autofac;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers.Mailing;
using Shared.Persistance.Implamantations;
using Shared.Persistance.Interfaces;

namespace AccountApi.Infrastructure.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region helper
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<AccountMailSenderStrategy>().As<IMailSenderStrategy>();
            builder.RegisterType<EfUnitOfWork<AccountDbContext>>().As<IUnitOfWork>();
            #endregion

            #region managers&Repositories
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<UserOperationClaimRepository>().As<IUserOperationClaimRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<OperationClaimRepository>().As<IOperationClaimRepository>();
            builder.RegisterType<EmailTemplateManager>().As<ITemplateMailService>();
            builder.RegisterType<EfMailTemplateDal>().As<IEfMailTemplateDal>();
            builder.RegisterType<EfFailureLogDal>().As<IEfFailureLogDal>();
            builder.RegisterType<FailureLogManager>().As<IFailureLogService>();
            #endregion

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<AccountDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new AccountDbContext(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();
            

            #region validators
            builder.RegisterType<ClaimValidator>().As<IValidator<ClaimDto>>().InstancePerLifetimeScope();
            builder.RegisterType<UserClaimValidator>().As<IValidator<UserClaimDto>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterUserValidator>().As<IValidator<UserForRegisterDto>>().InstancePerLifetimeScope();
            builder.RegisterType<TemplateAddValidator>().As<IValidator<TemplateAddDto>>().InstancePerLifetimeScope();
            builder.RegisterType<TemplateUpdateValidator>().As<IValidator<TemplateUpdateDto>>().InstancePerLifetimeScope();
            #endregion

        }
    }
}
