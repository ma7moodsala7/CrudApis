using MediatR;

namespace Application.Features.AppUser.Commands.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
