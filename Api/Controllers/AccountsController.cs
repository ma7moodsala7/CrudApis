using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.AppUser.Commands.Login;
using Application.Features.AppUser.Commands.Register;
using MediatR;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
        {
            var registrationResponse = await _mediator.Send(registrationRequest).ConfigureAwait(false);
            return Ok(registrationResponse);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var loginResponse = await _mediator.Send(loginRequest).ConfigureAwait(false);
            return Ok(loginResponse);
        }

        //[HttpPost("logout")]
        //public async Task<ActionResult> LogoutAsync(LogoutResponse logoutRequest)
        //{
        //    _ = await _mediator.Send(loginRequest).ConfigureAwait(false)
        //    return Ok();
        //}



    }
}
