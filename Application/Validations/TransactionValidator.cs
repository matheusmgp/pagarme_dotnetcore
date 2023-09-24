using Application.Dtos;
using FluentValidation;

namespace Application.Validations
{
    public class TransactionValidator : AbstractValidator<TransactionDto>
    {
        public TransactionValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Price deve ser informado.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description deve ser informado.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty()
                .NotNull()
                .WithMessage("PaymentMethod deve ser informado.");

            RuleFor(x => x.CardNumber)
               .NotEmpty()
               .NotNull()
               .WithMessage("CardNumber deve ser informado.");

            RuleFor(x => x.OwnerName)
              .NotEmpty()
              .NotNull()
              .WithMessage("OwnerName deve ser informado.");

            RuleFor(x => x.Cvv)
              .NotEmpty()
              .NotNull()
              .WithMessage("Cvv deve ser informado.");

            RuleFor(x => x.CardExpiresDate)
             .NotEmpty()
             .NotNull()
             .WithMessage("CardExpiresDate deve ser informado.");
        }
    }
}
