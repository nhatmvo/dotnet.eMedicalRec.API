using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eMedicalRecords.API.Controllers
{
    using Applications.Commands.Template;
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTemplateForDocument([FromBody] CreateTemplateCommand templateCommand,
            [FromHeader(Name = "X-Request-Id")] string requestId)
        {
            var commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                commandResult = await _mediator.Send(templateCommand);
            }

            if (!commandResult)
                return BadRequest();
            return Ok();
        }
    }
}