using FluentValidation;
using FluentValidation.Validators;
using Onboarding.Models;

namespace Onboarding.Validations
{
    public class EnrollmentMessagesValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentMessagesValidator(DatabaseContext databaseContext)
        {
            RuleFor(enrollment => enrollment.FinanceData).SetValidator(new FinanceDataMessagesValidator(databaseContext));
            RuleFor(enrollment => enrollment.PersonalData).SetValidator(new PersonalDataMessagesValidator(databaseContext));
        }
    }
}
