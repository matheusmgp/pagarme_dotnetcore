using Application.Dtos;
using FluentValidation;

namespace Application.Validations
{
    public class PayableValidation : AbstractValidator<PayableDto>
    {
        public PayableValidation()
        {
            RuleFor(x => x.Amount)
                .NotEmpty()
                .NotNull()
                .WithMessage("Amount deve ser informado.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .NotNull()
                .WithMessage("Status deve ser informado.");

            RuleFor(x => x.Availability)
                .NotEmpty()
                .NotNull()
                .WithMessage("Availability deve ser informado.");

            RuleFor(x => x.PaymentDate)
               .NotEmpty()
               .NotNull()
               .WithMessage("PaymentDate deve ser informado.");

            RuleFor(x => x.TransactionId)
              .NotEmpty()
              .NotNull()
              .WithMessage("TransactionId deve ser informado.");


        }
    }
}
