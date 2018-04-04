using FluentValidation;
using FluentValidation.Validators;
using Onboarding.Models;

namespace Onboarding.Validations
{
    public class EnrollmentMessagesValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentMessagesValidator(DatabaseContext databaseContext)
        {
            RuleFor(enrollment => enrollment).Custom((enrollment, context) =>
            {
                databaseContext.Entry(enrollment).Reference(x => x.FinanceData).Load();
                databaseContext.Entry(enrollment.FinanceData).Reference(x => x.Plan).Load();
                databaseContext.Entry(enrollment.FinanceData).Collection(x => x.Guarantors).Load();

                CheckPlan(enrollment, context);
            });
        }

        private void CheckPlan(Enrollment enrollment, CustomContext context)
        {
            if (enrollment.FinanceData.Plan != null && enrollment.FinanceData.Plan.Guarantors > 0)
            {
                if (enrollment.FinanceData.Plan.Guarantors > enrollment.FinanceData.Guarantors.Count)
                {
                    context.AddFailure(string.Format("Para o plano {0} é necessário preencher {1} fiador(es)", enrollment.FinanceData.Plan.Name, enrollment.FinanceData.Plan.Guarantors));
                }
            }
        }
    }
}
