using FluentValidation;

namespace Onboarding.Validations
{
    public class DocumentValidator : AbstractValidator<Models.Document>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.DocumentTypeId).NotEmpty();
            RuleFor(x => x.Url).NotEmpty();
        }
    }
}
