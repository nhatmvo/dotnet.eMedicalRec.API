using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eMedicalRecords.API.Applications.Commands.Document;
using eMedicalRecords.API.Applications.Queries.DocumentQueries;
using eMedicalRecords.Infrastructure.Securities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eMedicalRecords.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDocumentQueries _documentQueries;

        public DocumentsController(IMediator mediator, IDocumentQueries documentQueries)
        {
            _mediator = mediator;
            _documentQueries = documentQueries;
        }

        [HttpPost("submission")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PopulateData([FromForm] PopulateDataCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result)
                    return BadRequest();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong when handling form submission");
            }
            
        }

        [HttpGet("entries")]
        [ProducesResponseType(typeof(DocumentEntryData), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> FetchEntryData([FromQuery]string id)
        {
            try
            {
                var result = await _documentQueries.GetEntryData(Guid.Parse(id));
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
        }


        [HttpPatch("submission")]
        public async Task<IActionResult> SaveImage([FromForm]UpdateDataCommand data)
        {
            throw new NotImplementedException();
        }
        
    }
}