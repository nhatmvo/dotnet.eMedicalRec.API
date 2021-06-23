using System.Net;
using System.Threading.Tasks;
using eMedicalRecords.API.Applications.Commands.Account;
using eMedicalRecords.Infrastructure.Securities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMedicalRecords.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginCommand loginCommand)
        {
            try
            {
                var result = await _mediator.Send(loginCommand);
                return Ok(result);
            }
            catch
            {
                return BadRequest($"Incorrect password for user: {loginCommand.UserName}");
            }
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        [Authorize(Roles = "superuser")]
        public async Task<ActionResult<LoginDTO>> Register([FromBody] RegisterCommand registerCommand)
        {
            return await _mediator.Send(registerCommand);
        }
    }
}