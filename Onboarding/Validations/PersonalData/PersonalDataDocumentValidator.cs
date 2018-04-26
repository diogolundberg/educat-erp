using FluentValidation;

namespace onboarding.Validations.PersonalData
{
    public class PersonalDataDocumentValidator : AbstractValidator<Models.PersonalDataDocument>
    {
        public PersonalDataDocumentValidator()
        {
            RuleFor(x => x.Document).SetValidator(x => new DocumentValidator());
        }
    }
}
