using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure;
using eMedicalRecords.Infrastructure.Securities;
using MediatR;

namespace eMedicalRecords.API.Applications.Commands.Account
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginDTO>
    {
        private readonly MedicalRecordContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(MedicalRecordContext context,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        
        public async Task<LoginDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var isExist = _context.Accounts.Any(a => a.Username == request.Username);
            if (isExist)
                throw new ArgumentException($"Username: {request.Username} has already existed in the system");

            var requestCreatedSuperuser = _context.Accounts.Any(a =>
                string.Equals(a.Role, request.Username, StringComparison.InvariantCultureIgnoreCase));
            if (requestCreatedSuperuser)
                throw new DataException($"SuperUser role is unique within the system! Cannot create username: {request.Username} w/ role as SuperUser");
            
            var salt = Guid.NewGuid().ToByteArray();
            await _context.Accounts.AddAsync(new AccountModel
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Salt = salt,
                Hash = _passwordHasher.Hash(request.Password, salt),
                Role = request.Role.ToLowerInvariant()
            }, cancellationToken);

            var claimsIdentity = await _jwtTokenGenerator.GetClaimsIdentity(request.Username, request.Role);
            var token = _jwtTokenGenerator.CreateToken(claimsIdentity);
            return new LoginDTO
            {
                Username = request.Username,
                JwtToken = token 
            };
        }
    }
}