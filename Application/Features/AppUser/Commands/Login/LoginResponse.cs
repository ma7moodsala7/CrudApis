namespace Application.Features.AppUser.Commands.Login
{
    public class LoginResponse
    {
        public string Username { get; set; }

        public string BearerToken { get; set; }
    }
}