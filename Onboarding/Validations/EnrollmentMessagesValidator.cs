using FluentValidation;
using Onboarding.Models;

namespace Onboarding.Validations
{
    public class EnrollmentMessagesValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentMessagesValidator(DatabaseContext databaseContext)
        {

        }
    }
}
