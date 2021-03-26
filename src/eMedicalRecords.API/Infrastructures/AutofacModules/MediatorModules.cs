using Autofac;
using eMedicalRecords.API.Applications.Behaviors;
using MediatR;

namespace eMedicalRecords.API.Infrastructures.AutofacModules
{
    public class MediatorModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(TransactionPipelineBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerRequest();
        }
    }
}