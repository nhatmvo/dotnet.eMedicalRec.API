using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eMedicalRecords.Infrastructure;
using eMedicalRecords.Infrastructure.Exceptions;
using eMedicalRecords.Infrastructure.Securities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMedicalRecords.API.Applications.Commands.Account
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDTO>
    {
        private readonly MedicalRecordContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(MedicalRecordContext context, 
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        
        public async Task<LoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(p => p.Username == request.UserName, cancellationToken);
            if (account == null)
                throw new RecordNotFoundException($"Account with username: {request.UserName} does not exist");

            if (account.Hash.SequenceEqual(_passwordHasher.Hash(request.Password, account.Salt)))
                throw new ArgumentException($"Incorrect password for username: {request.UserName}");

            var claimsIdentity = await _jwtTokenGenerator.GetClaimsIdentity(account.Username, account.Role);
            var token = _jwtTokenGenerator.CreateToken(claimsIdentity);
            return new LoginDTO
            {
                Username = request.UserName,
                JwtToken = token
            };
        }
    }

    public record LoginDTO
    {
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}