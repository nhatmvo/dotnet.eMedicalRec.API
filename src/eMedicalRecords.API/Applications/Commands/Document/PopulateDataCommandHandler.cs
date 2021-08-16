using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    using Domain.AggregatesModel.DocumentAggregate;
    using Domain.AggregatesModel.PatientAggregate;
    using Domain.AggregatesModel.TemplateAggregate;
    
    public class PopulateDataCommandHandler : IRequestHandler<PopulateDataCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IPatientRepository _patientRepository;

        public PopulateDataCommandHandler(IDocumentRepository documentRepository,
            ITemplateRepository templateRepository,
            IHostEnvironment hostEnvironment,
            IPatientRepository patientRepository)
        {
            _documentRepository = documentRepository;
            _templateRepository = templateRepository;
            _hostEnvironment = hostEnvironment;
            _patientRepository = patientRepository;
        }
        
        public async Task<bool> Handle(PopulateDataCommand request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.FindById(request.DocumentId);
            if (document == null)
                throw new ArgumentException($"There's no document with id: {request.DocumentId} for patient: {request.PatientId}");
            
            var entryToAdd = new Entry(request.TemplateId);
            document.AddEntry(entryToAdd);
            _documentRepository.Update(document);

            foreach (var data in request.EntryDataRequests)
            {
                data.Values ??= new List<string>();
                if (data.Files is { Count: > 0 })
                    foreach (var file in data.Files.Where(file => file.Length > 0))
                        data.Values.Add(await SaveFileToLocal(file));
                else
                    await ElementSubmissionValidation(data.ElementId, data.Values);
            }
            
            var dataBulk = request.EntryDataRequests
                .Select(edr => new EntryData(entryToAdd, edr.ElementId, edr.Values))
                .ToList();
            
            await _documentRepository.SubmitEntryData(dataBulk);
            await _documentRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            return true;
        }

        private async Task<string> SaveFileToLocal(IFormFile file)
        {
            using var streamReader = new StreamReader(file.OpenReadStream());
            var now = DateTime.UtcNow;
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = CalculateMD5(streamReader.BaseStream);
            var fileNameWithExtension = string.Concat(fileName, fileExtension);
            var relativeFilePath = Path.Combine(now.Year.ToString(),
                string.Concat(now.Month.ToString().PadLeft(2, '0'), now.Day.ToString().PadLeft(2, '0')), fileNameWithExtension);
            var absolutePath = Path.Combine(_hostEnvironment.ContentRootPath, "images", relativeFilePath);
            
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath) ?? string.Empty);
            await using Stream fileStream = new FileStream(absolutePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
 
            return relativeFilePath.Replace("\\", "");
        }

        private string CalculateMD5(Stream stream)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private async Task<string> GetDirectoryForPatient(Guid patientId, string entryDate)
        {
            var patient = await _patientRepository.FindPatientById(patientId);
            var patientFolderName = string.Concat(patient.GetPatientName.Replace(" ", string.Empty), "_",
                patient.GetIdentityNo);
            var directory = Path.Combine(_hostEnvironment.ContentRootPath, "upload", patientFolderName, entryDate);
            Directory.CreateDirectory(directory);
            return directory;
        }

        private async Task ElementSubmissionValidation(Guid elementId, List<string> submittedValues)
        {
            var elementBase = await _templateRepository.GetElementValidationTypeById(elementId);
            foreach (var submittedValue in submittedValues)
            {
                switch (elementBase.ElementType.Id)
                {
                    case (int) ElementTypeEnum.Checkbox:
                        var elementCheckbox = elementBase as ElementCheckbox;
                        elementCheckbox?.SetValues(submittedValue.Split(",").ToList());
                        elementCheckbox?.ValidatePopulatedData();
                        break;
                    case (int) ElementTypeEnum.Text:
                        var elementText = elementBase as ElementText;
                        elementText?.SetValue(submittedValue);
                        elementText?.ValidateLength();
                        elementText?.ValidateRestrictionLevel();
                        elementText?.ValidateCustomExpression();
                        break;
                    case (int) ElementTypeEnum.RadioButton:
                        var elementRadioButton = elementBase as ElementRadioButton;
                        elementRadioButton?.SetValue(submittedValue);
                        elementRadioButton?.ValidatePopulatedData();
                        break;
                }
            }

        }   
    }
}