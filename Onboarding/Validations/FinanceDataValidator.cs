using FluentValidation;
using Onboarding.Models;

namespace Onboarding.Validations
{
    public class FinanceDataValidator : AbstractValidator<FinanceData>
    {
        public FinanceDataValidator()
        {
            RuleFor(financeData => financeData.Representative).SetValidator(new RepresentativeValidator());
            RuleFor(financeData => financeData.Guarantors).SetCollectionValidator(new GuarantorValidator());
        }
    }
    
}
