using Autofac;
using EventBus.Abstractions;
using Nvg.SMSBackgroundTask.EventHandler;
using System.Reflection;

namespace Nvg.SMSBackgroundTask.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SendSMSEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
