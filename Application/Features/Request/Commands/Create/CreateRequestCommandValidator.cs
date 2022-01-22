using FluentValidation;

namespace Application.Features.Request.Commands.Create
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(p => p.CreateRequestDto.Subject)
                .NotEmpty().WithMessage("'{PropertyName}' is required")
                .NotNull();

            RuleFor(p => p.CreateRequestDto.Details)
                .NotEmpty().WithMessage("'{PropertyName}' is required")
                .NotNull();
        }
    }
}
