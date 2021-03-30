using System.Transactions;
using Autofac;
using eMedicalRecords.API.Applications.Behaviors;
using eMedicalRecords.Domain.AggregatesModel.DocumentAggregate;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using eMedicalRecords.Infrastructure.Repositories;
using MediatR;

namespace eMedicalRecords.API.Infrastructures.AutofacModules
{
    public class ApplicationModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentRepository>()
                .As<IDocumentRepository>()
                .InstancePerDependency();
            
            builder.RegisterType<TemplateRepository>()
                .As<ITemplateRepository>()
                .InstancePerDependency();
        }
    }
}