using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public class PaymentStatus : BaseStatus<Payment>
    {
        public PaymentStatus(AbstractValidator<Payment> validator, Payment entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (!string.IsNullOrEmpty(_entity.BilletUrl) && !string.IsNullOrEmpty(_entity.Url))
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }
    }
}
