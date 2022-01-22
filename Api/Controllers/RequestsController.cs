using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Request.Commands.Create;
using Application.Features.Request.Commands.Delete;
using Application.Features.Request.Commands.Update;
using Application.Features.Request.Queries.GetAll;
using Application.Features.Request.Queries.GetById;
using Application.Features.Request.Queries.GetByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Guid _authenticatedUserId;

        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
            _authenticatedUserId = User != null ? new Guid(User.FindFirstValue("uid")) : Guid.Empty;
        }

        #region Queries

        [HttpGet]
        public async Task<ActionResult<GetAllRequestsVm>> GetAll(int? pageSize, int? pageNo)
        {
            return await _mediator.Send(new GetAllRequestsQuery(pageSize, pageNo)).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestByIdVm>> GetById(Guid id)
        {
            return await _mediator.Send(new GetRequestByIdQuery(id)).ConfigureAwait(false);
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<UserRequestsVm>> GetAllByUserId(Guid userId)
        {
            return await _mediator.Send(new GetRequestsByUserQuery(userId)).ConfigureAwait(false);
        }


        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto createRequestDto)
        {
            return StatusCode(201, 
                await _mediator.Send(new CreateRequestCommand(_authenticatedUserId, createRequestDto))
                    .ConfigureAwait(false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRequestDto updateRequestDto)
        {
            return Ok(
                await _mediator.Send(new UpdateRequestCommand(_authenticatedUserId, id, updateRequestDto))
                    .ConfigureAwait(false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRequestCommand(id)).ConfigureAwait(false);
            return NoContent();
        }

        #endregion
    }
}
