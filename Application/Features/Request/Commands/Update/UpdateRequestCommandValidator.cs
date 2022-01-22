using FluentValidation;

namespace Application.Features.Request.Commands.Update
{
    public class UpdateRequestCommandValidator : AbstractValidator<UpdateRequestCommand>
    {
        public UpdateRequestCommandValidator()
        {
            RuleFor(p => p.RequestId)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.UpdateRequestDto.Subject)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.UpdateRequestDto.Details)
                .NotEmpty()
                .NotNull();
        }
    }
}
