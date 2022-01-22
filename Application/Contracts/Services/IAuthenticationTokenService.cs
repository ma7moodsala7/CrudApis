using Domain.Entities;

namespace Application.Contracts.Services
{
    public interface IAuthenticationTokenService
    {
        string CreateToken(AppUser user);
    }
}
