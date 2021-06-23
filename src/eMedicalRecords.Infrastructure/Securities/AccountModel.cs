using System;

namespace eMedicalRecords.Infrastructure.Securities
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        
        public byte[] Hash { get; set; }
        public string Role { get; set; }
    }
}