using FluentValidation;

namespace onboarding.Validations.Payment
{
    public class PaymentValidator : AbstractValidator<Models.Payment>
    {

        public PaymentValidator()
        {
            RuleFor(contract => contract.BilletUrl).NotEmpty().WithName(Resources.Models.Payment.BilletUrl);
            RuleFor(contract => contract.Url).NotEmpty().WithName(Resources.Models.Payment.Url);
        }
    }
}
