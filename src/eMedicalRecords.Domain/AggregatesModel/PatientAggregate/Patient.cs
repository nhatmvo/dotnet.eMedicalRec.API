using System;
using eMedicalRecords.Domain.Events;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.PatientAggregate
{
    public class Patient : Entity, IAggregateRoot
    {
        public string PatientNo { get; set; }
        private string _identityNo;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _email;
        private string _phoneNumber;
        private DateTime _dateOfBirth;
        private bool _hasInsurance;
        private string _description;

        public PatientAddress PatientAddress { get; private set; }
        
        public IdentityType IdentityType { get; private set; }
        private int _identityTypeId;
        
        public string FullName => _lastName + ' ' + _middleName + ' ' + _firstName;

        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Patient()
        {
            _hasInsurance = false;
        }

        public Patient(string patientIdentifier, string identityNo, int identityTypeId, string firstName, string lastName, string middleName, string email, string phoneNumber,
            DateTime dateOfBirth, PatientAddress patientAddress, bool hasInsurance, string description)
        {
            PatientNo = patientIdentifier;
            _identityNo = identityNo;
            _identityTypeId = identityTypeId;
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            _email = email;
            _phoneNumber = phoneNumber;
            _dateOfBirth = dateOfBirth;
            PatientAddress = patientAddress;
            _hasInsurance = hasInsurance;
            _description = description;
            
            AddPatientAddedDomainEvent(PatientNo, _identityNo, _email);
        }

        private void AddPatientAddedDomainEvent(string patientNo, string identityNo, string email)
        {
            var patientAddedDomainEvent = new PatientAddedDomainEvent(this, patientNo, identityNo, email);
            AddDomainEvent(patientAddedDomainEvent);
        }

        public void UpdatePatientInformation()
        {
            
        }

    }
}