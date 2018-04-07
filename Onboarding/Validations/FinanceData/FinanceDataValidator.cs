using FluentValidation;

namespace Onboarding.Validations.FinanceData
{
    public class FinanceDataValidator : AbstractValidator<Models.FinanceData>
    {
        public FinanceDataValidator()
        {
            RuleFor(financeData => financeData.Representative).SetValidator(new RepresentativeValidator());
            RuleFor(financeData => financeData.Guarantors).SetCollectionValidator(new GuarantorValidator());
            RuleFor(financeData => financeData.PlanId).NotNull();
            RuleFor(financeData => financeData.PaymentMethodId).NotNull();
        }
    }
    
}
