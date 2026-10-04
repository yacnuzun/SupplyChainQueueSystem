using Autofac;
using BillApi.Application.Services.Implemantations;
using BillApi.Application.Services.Interfaces;
using BillApi.Infrastructure.Data.DbConnectionContext;
using BillApi.Infrastructure.Repositories.Implemantations;
using BillApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Persistance.Entities;
using Shared.Persistance.Implamantations;
using Shared.Persistance.Interfaces;

namespace BillApi.Infrastructure.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BillManager>().As<IBillService>();
            builder.RegisterType<EfBillRepository>().As<IBillRepository>();
            builder.RegisterType<EfUnitOfWork<BillDbContext>>().As<IUnitOfWork>();
            
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = new DbContextOptionsBuilder<BillDbContext>()
                    .UseNpgsql(configuration["DbConnection:ConnectionString"])
                    .Options;
                return new BillDbContext(opts);
            })
.AsSelf()
.InstancePerLifetimeScope();
            


        }
    }
}
