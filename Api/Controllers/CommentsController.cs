using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Comment.Commands.Create;
using Application.Features.Comment.Commands.Delete;
using Application.Features.Comment.Commands.Update;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        #region Commands

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromForm] CreateCommentDto createCommentDto)
        {
            var createCommentCommand = _mapper.Map<CreateCommentCommand>(createCommentDto);
            createCommentCommand.UserGuid = new Guid(User.FindFirstValue("uid"));
            var response = await _mediator.Send(createCommentCommand).ConfigureAwait(false);
            return StatusCode(201, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] string value)
        {
            var updateCommentCommand = new UpdateCommentCommand
            {
                CommentId = id, 
                Body = value,
                UserGuid = new Guid(User.FindFirstValue("uid"))
            };
            var response = await _mediator.Send(updateCommentCommand).ConfigureAwait(false);
            return StatusCode(201, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var deleteCommentCommand = new DeleteCommentCommand() { CommentId = id };
            await _mediator.Send(deleteCommentCommand).ConfigureAwait(false);

            return NoContent();
        }


        #endregion




    }
}
