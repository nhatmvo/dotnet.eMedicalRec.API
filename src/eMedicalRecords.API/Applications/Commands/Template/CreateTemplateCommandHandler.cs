using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Template
{
    
    using Domain.AggregatesModel.DocumentAggregate;
    using Domain.AggregatesModel.TemplateAggregate;
    
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, bool>
    {
        private readonly ITemplateRepository _templateRepository;

        public CreateTemplateCommandHandler(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }
        
        public async Task<bool> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var headingSet = new Template(request.Name, request.Description);
            headingSet.AddSections(AddSectionsAndItsChildFromRequest(request.SectionRequests));
            
            await _templateRepository.AddTemplate(headingSet);
            await _templateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }

        private List<Section> AddSectionsAndItsChildFromRequest(List<SectionRequest> sections, Guid? parentSectionId = null)
        {
            var result = new List<Section>();
            foreach (var section in sections)
            {
                var sectionToAdd = new Section(section.Name, section.Tooltip, section.KindId, section.KindValues,
                    parentSectionId);
                result.Add(sectionToAdd);
                if (section.ChildSections != null && section.ChildSections.Any())
                {
                    result.AddRange(AddSectionsAndItsChildFromRequest(section.ChildSections, sectionToAdd.Id));
                }
            }
            return result;
        }
    }
}