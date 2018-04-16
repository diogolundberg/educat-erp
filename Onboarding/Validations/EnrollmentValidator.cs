using FluentValidation;
using FluentValidation.Validators;
using Onboarding.Models;

namespace Onboarding.Validations
{
    public class EnrollmentValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentValidator(DatabaseContext databaseContext)
        {
            RuleFor(enrollment => enrollment.FinanceData).SetValidator(new FinanceDataMessagesValidator(databaseContext));
        }
    }
}
