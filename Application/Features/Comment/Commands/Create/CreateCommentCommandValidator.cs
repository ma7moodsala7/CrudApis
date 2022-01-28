using FluentValidation;

namespace Application.Features.Comment.Commands.Create
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(e => e.Body)
                .NotNull()
                .NotEmpty();

            RuleFor(e => e.RequestId)
                .NotNull()
                .NotEmpty();
        }
    }
}
