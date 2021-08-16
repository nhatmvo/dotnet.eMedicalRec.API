using Autofac;
using eMedicalRecords.API.Applications.Queries.DocumentQueries;
using eMedicalRecords.API.Applications.Queries.PatientQueries;
using eMedicalRecords.API.Projections;
using eMedicalRecords.Domain.AggregatesModel.PatientAggregate;
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
        public string QueryConnectionString { get; }

        public ApplicationModules(string queryConnectionString)
        {
            QueryConnectionString = queryConnectionString;
        }
        
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

            builder.Register(b => new PatientQueries(QueryConnectionString))
                .As<IPatientQueries>()
                .InstancePerLifetimeScope();

            builder.Register(b => new DocumentQueries(QueryConnectionString))
                .As<IDocumentQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PatientRepository>()
                .As<IPatientRepository>()
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