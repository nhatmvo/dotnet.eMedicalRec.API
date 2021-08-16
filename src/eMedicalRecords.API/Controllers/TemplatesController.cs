using System;
using System.Net;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure.Securities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace eMedicalRecords.API.Controllers
{
    using Applications.Commands.Template;
    using Applications.Queries.TemplateQueries;
    
    [Route("api/v1/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public class TemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITemplateQueries _templateQueries;

        public TemplatesController(IMediator mediator, ITemplateQueries templateQueries)
        {
            _mediator = mediator;
            _templateQueries = templateQueries;
        }
        
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTemplateForDocument([FromBody] CreateTemplateCommand templateCommand)
        {
            var commandResult = await _mediator.Send(templateCommand);

            if (string.IsNullOrEmpty(commandResult))
                return BadRequest();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(TemplateDTO), (int) HttpStatusCode.OK)]
        [ActionName("GetTemplateAsync")]
        public async Task<ActionResult<TemplateDTO>> GetTemplateAsync(Guid id)
        {
            var result = await _templateQueries.GetTemplateAsync(id);
            return TemplateDTO.FromTemplateBson(result);
        }
    }
}