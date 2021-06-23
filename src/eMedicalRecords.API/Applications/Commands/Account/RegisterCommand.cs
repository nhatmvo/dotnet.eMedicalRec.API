using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Account
{
    public class RegisterCommand : IRequest<LoginDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}