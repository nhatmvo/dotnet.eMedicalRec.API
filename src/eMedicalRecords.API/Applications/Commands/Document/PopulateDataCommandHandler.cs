using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Domain.AggregatesModel.TemplateAggregate;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Document
{
    using Domain.AggregatesModel.DocumentAggregate;
    
    public class PopulateDataCommandHandler : IRequestHandler<PopulateDataCommand, bool>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ITemplateRepository _templateRepository;

        public PopulateDataCommandHandler(IDocumentRepository documentRepository,
            ITemplateRepository templateRepository)
        {
            _documentRepository = documentRepository;
            _templateRepository = templateRepository;
        }
        
        public async Task<bool> Handle(PopulateDataCommand request, CancellationToken cancellationToken)
        {
            foreach (var data in request.EntryDataRequests)
            {
                await ElementSubmissionValidation(data.ElementId, data.Value);
            }
            
            var dataBulk = request.EntryDataRequests
                .Select(edr => new EntryData(request.EntryId, edr.ElementId, edr.Value))
                .ToList();
            
            await _documentRepository.SubmitEntryData(dataBulk);
            await _documentRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
            return true;
        }

        private async Task ElementSubmissionValidation(Guid elementId, string submittedValue)
        {
            var elementBase = await _templateRepository.GetElementValidationTypeById(elementId);
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