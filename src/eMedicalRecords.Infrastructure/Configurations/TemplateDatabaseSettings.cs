namespace eMedicalRecords.Infrastructure.Configurations
{
    public class TemplateDatabaseSettings : ITemplateDatabaseSettings
    {
        public string TemplatesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}