using Autofac;
using FinancialAPI.Infrastructure.Repositories.Implemantations;
using FinancialAPI.Infrastructure.Repositories.Interfaces;

namespace FinancialAPI.Infrastructure.DependencyResolver.AutofacHelper
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<FinancialHelper>().As<IFinancialHelper>();

        }
    }
}
