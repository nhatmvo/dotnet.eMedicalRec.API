namespace eMedicalRecords.API.Infrastructures.Services
{
    public interface IIdentityService
    {
        public string GetUserIdentity();

        public string GetUserName();
    }
}