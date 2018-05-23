using FluentValidation;

namespace onboarding.Validations.FinanceData
{
    public class GuarantorDocumentsValidator : AbstractValidator<Models.GuarantorDocument>
    {
        public GuarantorDocumentsValidator()
        {
            RuleFor(x => x.Document).SetValidator(x => new DocumentValidator());
        }
    }
}
