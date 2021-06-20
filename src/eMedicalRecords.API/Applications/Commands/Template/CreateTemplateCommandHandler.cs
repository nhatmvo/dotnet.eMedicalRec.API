using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Applications.Commands.Template
{
    using Domain.AggregatesModel.TemplateAggregate;
    
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, string>
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ILogger<CreateTemplateCommandHandler> _logger;

        public CreateTemplateCommandHandler(ITemplateRepository templateRepository,
            ILogger<CreateTemplateCommandHandler> logger)
        {
            _templateRepository = templateRepository;
            _logger = logger;
        }
        
        public async Task<string> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var sectionsBeAdded = FlattenNestedElement(request.Elements);
            var template = new Template(request.Name, request.Description, sectionsBeAdded);
            
            await _templateRepository.AddTemplate(template);
            template.WrapUp();

            await _templateRepository
                .UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return template.Id.ToString();

        }

        private List<ElementBase> FlattenNestedElement(List<ElementProperty> elements, Guid? parentSectionId = null)
        {
            var result = new List<ElementBase>();
            foreach (var element in elements)
            {
                ElementBase elementToAdd = null;
                switch (element.ElementTypeId)
                {
                    case (int) ElementTypeEnum.Checkbox:
                        elementToAdd = InitCheckbox(element, parentSectionId);
                        break;
                    case (int) ElementTypeEnum.RadioButton:
                        elementToAdd = InitRadioButton(element, parentSectionId);
                        break;
                    case (int) ElementTypeEnum.Text:
                        elementToAdd = InitText(element, parentSectionId);
                        break;
                }
                result.Add(elementToAdd);
                
                if (element.ChildElements != null && element.ChildElements.Any())
                    result.AddRange(FlattenNestedElement(element.ChildElements, elementToAdd?.Id));
            }
            return result;
        }

        private ElementBase InitCheckbox(ElementProperty elementRequest, Guid? parentElementId)
        {
            if (!(elementRequest is CheckboxProperty checkboxProperty))
                throw new ArgumentNullException("");
            return new ElementCheckbox(elementRequest.Name, elementRequest.ElementTypeId, elementRequest.Tooltip,
                elementRequest.Description, parentElementId, checkboxProperty.Values);
        }

        private ElementBase InitRadioButton(ElementProperty elementRequest, Guid? parentElementId)
        {
            if (!(elementRequest is RadiobuttonProperty radioButtonProperty))
                throw new ArgumentNullException("");
            return new ElementRadioButton(elementRequest.Name, elementRequest.ElementTypeId, elementRequest.Tooltip,
                elementRequest.Description, parentElementId, radioButtonProperty.Values);   
        }

        private ElementBase InitText(ElementProperty elementRequest, Guid? parentElementId)
        {
            if (!(elementRequest is TextProperty textProperty))
                throw new ArgumentNullException("");
            var elementText = new ElementText(elementRequest.Name, elementRequest.ElementTypeId, elementRequest.Tooltip,
                elementRequest.Description, parentElementId);
            elementText.SetValidationProperties(textProperty.MinimumLength, textProperty.MaximumLength, textProperty.TextRestrictionLevel, textProperty.CustomExpression);
            return elementText;
        }
    }
}