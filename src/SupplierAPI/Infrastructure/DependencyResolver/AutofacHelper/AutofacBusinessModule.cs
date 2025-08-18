using Autofac;
using Microsoft.EntityFrameworkCore;
using SupplierAPI.Infrastructure.DbConectionContext;
using SupplierAPI.Infrastructure.Repositories.Implemantations;
using SupplierAPI.Infrastructure.Repositories.Interfaces;

namespace SupplierAPI.Infrastructure.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<SupplierHelper>().As<ISupplierHelper>();
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<SuplierDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new SuplierDbContext(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();
        }
    }
}
