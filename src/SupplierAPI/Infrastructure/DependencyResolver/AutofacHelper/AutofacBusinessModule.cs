using Autofac;
using SupplierAPI.Infrastructure.Repositories.Implemantations;
using SupplierAPI.Infrastructure.Repositories.Interfaces;

namespace SupplierAPI.Infrastructure.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<SupplierHelper>().As<ISupplierHelper>();

        }
    }
}
