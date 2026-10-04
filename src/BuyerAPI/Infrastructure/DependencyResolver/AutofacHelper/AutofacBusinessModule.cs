using Autofac;
using BuyerAPI.Infrastructure.Data.DbConnectionContext;
using BuyerAPI.Infrastructure.Repositories.Implemantations;
using BuyerAPI.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuyerAPI.Infrastructure.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BuyerHelper>().As<IBuyerHelper>();

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<BuyerDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new BuyerDbContext(opts);
            })
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
