using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Contracts.Services;
using Application.Exceptions;
using MediatR;

namespace Application.Features.AppUser.Commands.Register
{
    public class RegistrationRequestHandler : IRequestHandler<RegistrationRequest,RegistrationResponse>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IAuthenticationTokenService _authenticationTokenService;

        public RegistrationRequestHandler(IAppUserRepository userRepository, IAuthenticationTokenService authenticationTokenService)
        {
            _userRepository = userRepository;
            _authenticationTokenService = authenticationTokenService;
        }

        public async Task<RegistrationResponse> Handle(RegistrationRequest registrationRequest, CancellationToken cancellationToken)
        {
            // validate Registration Request
            var validationResult = await new RegistrationRequestValidator().ValidateAsync(registrationRequest, cancellationToken).ConfigureAwait(false);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            // check User Identity Existence
            if (await _userRepository.GetUserByNameAsync(registrationRequest.Username).ConfigureAwait(false) != null)
                throw new BadRequestException("This username already exist.");


            using var hmac = new HMACSHA512();
            var newAppUser = new Domain.Entities.AppUser
            {
                Username = registrationRequest.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationRequest.Password)),
                PasswordSalt = hmac.Key
            };

            newAppUser = await _userRepository.AddAsync(newAppUser).ConfigureAwait(false);

            return new RegistrationResponse
            {
                Username = newAppUser.Username,
                BearerToken = _authenticationTokenService.CreateToken(newAppUser)
            };
        }
    }
}
