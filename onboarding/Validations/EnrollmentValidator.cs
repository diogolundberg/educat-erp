using FluentValidation;
using FluentValidation.Validators;
using onboarding.Models;

namespace onboarding.Validations
{
    public class EnrollmentValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentValidator(DatabaseContext databaseContext)
        {
            RuleFor(enrollment => enrollment.FinanceData).SetValidator(new FinanceDataMessagesValidator(databaseContext));
        }
    }
}
