namespace eMedicalRecords.Infrastructure.Configurations
{
    public class DbConfiguration
    {
        public string PostgresHostname { get; set; }
        public string PostgresPort { get; set; }
        public string PostgresUsername { get; set; }
        public string PostgresPassword { get; set; }
        public string PostgresDatabase { get; set; }
    }
}