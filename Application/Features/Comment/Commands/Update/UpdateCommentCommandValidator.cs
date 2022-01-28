using FluentValidation;

namespace Application.Features.Comment.Commands.Update
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(p => p.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(p => p.Body)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();
        }
    }
}
