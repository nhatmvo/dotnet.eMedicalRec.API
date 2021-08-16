using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using eMedicalRecords.API.Infrastructures.Extensions;
using eMedicalRecords.Infrastructure.Securities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Controllers
{
    using Applications.Commands.Patient;
    using Applications.Queries.PatientQueries;
    
    [Route("api/v1/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
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
        
        [HttpGet("{patientId}/entries")]
        [ProducesResponseType(typeof(PatientDetails), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<PatientDetails>> GetPatientAsync(Guid patientId, Guid selectedElementId)
        {
            try
            {
                var patient = await _patientQueries.GetPatientAsync(patientId, selectedElementId);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<PatientView>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<PatientView>>> GetPatientsAsync([FromQuery]string filter)
        {
            try
            {
                var patients = await _patientQueries.GetPatientsAsync(filter);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(PatientView), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<PatientView>>> GetPatientByIdAsync([FromQuery]Guid id)
        {
            try
            {
                var patients = await _patientQueries.GetPatientsAsync(id.ToString());
                var patientViews = patients as PatientView[] ?? patients.ToArray();
                if (patientViews.Length == 1) 
                    return Ok(patientViews.FirstOrDefault());

                return NotFound();
            }
            catch (Exception ex)
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