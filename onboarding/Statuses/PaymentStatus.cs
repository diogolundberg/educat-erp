using FluentValidation;
using onboarding.ViewModels.Enrollments;

namespace onboarding.Statuses
{
    public class PaymentStatus : BaseStatus<Invoice>
    {
        public PaymentStatus(AbstractValidator<Invoice> validator, Invoice entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            return "invalid";
        }
    }
}
