using MediatR;

namespace Application.Features.AppUser.Commands.Register
{
    public class RegistrationRequest : IRequest<RegistrationResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
