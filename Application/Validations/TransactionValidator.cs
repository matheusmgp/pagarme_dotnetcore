using Application.Dtos;
using Application.Utils;
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
                .WithMessage("PaymentMethod deve ser informado.")
                .Must(x => x.Equals(PaymentMethodEnum.DEBIT) || x.Equals(PaymentMethodEnum.CREDIT))
                .WithMessage("O metodo de pagamento precisa ser debit_card ou credit_card");

            RuleFor(x => x.CardNumber)
               .NotEmpty().WithMessage("CardNumber deve ser informado.")
               .NotNull().WithMessage("CardNumber deve ser informado.")
               .MaximumLength(16).WithMessage("CardNumber deve ter no máximo 16 digitos.");

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
