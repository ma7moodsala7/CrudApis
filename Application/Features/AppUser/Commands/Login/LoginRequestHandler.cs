using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Exceptions;
using MediatR;

namespace Application.Features.AppUser.Commands.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest,LoginResponse>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IAuthenticationTokenService _authenticationTokenService;

        public LoginRequestHandler(IAppUserRepository userRepository, IAuthenticationTokenService authenticationTokenService)
        {
            _userRepository = userRepository;
            _authenticationTokenService = authenticationTokenService;
        }

        public async Task<LoginResponse> Handle(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            // check User Identity Existence
            var appUser = await _userRepository.GetUserByNameAsync(loginRequest.Username).ConfigureAwait(false);
            if (appUser == null)
                throw new BadRequestException("invalid username");

            // Validate User Password
            using var hmac = new HMACSHA512(appUser.PasswordSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != appUser.PasswordHash[i])
                    throw new BadRequestException("Invalid Password");
            }
            
            return new LoginResponse
            {
                Username = appUser.Username,
                BearerToken = _authenticationTokenService.CreateToken(appUser)
            };
        }
    }
}
