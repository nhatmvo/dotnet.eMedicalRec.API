using Autofac;
using eMedicalRecords.API.Projections;
using eMedicalRecords.Infrastructure.Securities;
using eMedicalRecords.Infrastructure.Services;

namespace eMedicalRecords.API.Infrastructures.AutofacModules
{
    using Applications.Queries.TemplateQueries;
    using Domain.AggregatesModel.DocumentAggregate;
    using Domain.AggregatesModel.TemplateAggregate;
    using Infrastructure.Repositories;
    
    public class ApplicationModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentRepository>()
                .As<IDocumentRepository>()
                .InstancePerDependency();
            
            builder.RegisterType<TemplateRepository>()
                .As<ITemplateRepository>()
                .InstancePerDependency();

            builder.RegisterType<TemplateQueries>()
                .As<ITemplateQueries>()
                .InstancePerDependency();

            builder.RegisterType<TemplateService>()
                .As<ITemplateService>()
                .SingleInstance();

            builder.RegisterType<MongoDbProjection>()
                .As<IStateProjection>()
                .SingleInstance();

            builder.RegisterType<PasswordHasher>()
                .As<IPasswordHasher>()
                .InstancePerDependency();

            builder.RegisterType<JwtTokenGenerator>()
                .As<IJwtTokenGenerator>()
                .InstancePerDependency();
        }
    }
}