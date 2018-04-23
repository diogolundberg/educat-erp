using FluentValidation;

namespace Onboarding.Validations
{
    public class DocumentValidator : AbstractValidator<Models.Document>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.DocumentTypeId).NotEmpty().WithName(onboarding.Resources.Models.Document.DocumentTypeId);
            RuleFor(x => x.Url).NotEmpty().WithName(onboarding.Resources.Models.Document.Url);
        }
    }
}
