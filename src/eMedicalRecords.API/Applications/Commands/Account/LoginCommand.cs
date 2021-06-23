using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Account
{
    public class LoginCommand : IRequest<LoginDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}