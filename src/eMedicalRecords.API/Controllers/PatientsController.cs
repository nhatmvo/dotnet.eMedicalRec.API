using System;
using System.Net;
using System.Threading.Tasks;
using eMedicalRecords.API.Infrastructures.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Controllers
{
    using Applications.Commands.Patient;
    using Applications.Queries;
    using Applications.Queries.PatientQueries;
    
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPatientQueries _patientQueries;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IMediator mediator, IPatientQueries patientQueries,
            ILogger<PatientsController> logger)
        {
            _mediator = mediator;
            _patientQueries = patientQueries;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatientAsync([FromBody] CreatePatientCommand createPatientCommand)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                createPatientCommand.GetGenericTypeName(),
                nameof(createPatientCommand.IdentityNo),
                createPatientCommand.IdentityNo,
                createPatientCommand);
            
            return await _mediator.Send(createPatientCommand);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(Patient), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<Patient>> GetPatientAsync(string identityNo, int identityTypeId)
        {
            try
            {
                var patient = await _patientQueries.GetPatientAsync(identityNo, identityTypeId);
                return Ok(patient);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatePatientAsync(UpdatePatientCommand updatePatientCommand)
        {
            _logger.LogInformation("----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                updatePatientCommand.GetGenericTypeName(),
                nameof(updatePatientCommand.IdentityNo),
                updatePatientCommand.IdentityNo,
                updatePatientCommand);

            var result = await _mediator.Send(updatePatientCommand);

            if (!result)
                return BadRequest();

            return Ok();
        }
    } 
}