using System;
using eMedicalRecords.Domain.Events;
using eMedicalRecords.Domain.SeedWorks;

namespace eMedicalRecords.Domain.AggregatesModel.PatientAggregate
{
    public class Patient : Entity, IAggregateRoot
    {
        public string PatientNo { get; set; }
        private string _identityNo;
        private string _name;
        private string _email;
        private bool _gender;
        private DateTime _admissionDate;
        private DateTime _examinationDate;
        private DateTime _surgeryDate;
        private string _phoneNumber;
        private DateTime _dateOfBirth;
        private bool _hasInsurance;

        public PatientAddress PatientAddress { get; private set; }
        
        public IdentityType IdentityType { get; private set; }
        private int _identityTypeId;

        public string GetIdentityNo => _identityNo;
        public string GetPatientName => _name;
        public override Guid Id { get; protected set; } = Guid.NewGuid();

        protected Patient()
        {
            _hasInsurance = false;
        }

        public Patient(string patientIdentifier, string identityNo, int identityTypeId, string name, bool gender, string email, string phoneNumber,
            DateTime dateOfBirth, PatientAddress patientAddress, bool hasInsurance, DateTime admissionDate, DateTime examinationDate, DateTime surgeryDate)
        {
            PatientNo = patientIdentifier;
            _identityNo = identityNo;
            _identityTypeId = identityTypeId;
            _name = name;
            _email = email;
            _gender = gender;
            _phoneNumber = phoneNumber;
            _dateOfBirth = dateOfBirth;
            PatientAddress = patientAddress;
            _hasInsurance = hasInsurance;
            _admissionDate = admissionDate;
            _examinationDate = examinationDate;
            _surgeryDate = surgeryDate;
            
            AddPatientAddedDomainEvent(Id, _identityNo, _email);
        }

        private void AddPatientAddedDomainEvent(Guid patientId, string identityNo, string email)
        {
            var patientAddedDomainEvent = new PatientAddedDomainEvent(this, patientId, identityNo, email);
            AddDomainEvent(patientAddedDomainEvent);
        }

        public void UpdatePatientInformation()
        {
            
        }

    }
}