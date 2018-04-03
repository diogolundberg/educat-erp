using FluentValidation;
using FluentValidation.Results;
using Onboarding.Models;
using System;

namespace Onboarding.Validations
{
    public class EnrollmentValidator : AbstractValidator<Enrollment>
    {
        public EnrollmentValidator()
        {
            RuleFor(enrollment => enrollment).Custom((enrollment, context) =>
            {
                int age = DateTime.Now.Year - enrollment.PersonalData.BirthDate.Value.Year;
                age = DateTime.Now.DayOfYear < enrollment.PersonalData.BirthDate.Value.DayOfYear ? age - 1 : age;

                RepresentativeValidator representativeValidator = new RepresentativeValidator();

                if (age < 18 && (enrollment.FinanceData == null || enrollment.FinanceData.Representative == null))
                {
                    context.AddFailure("financeData.representative", "Obrigatório prenchimento responsável.");
                }

                ValidationResult representativeResults = representativeValidator.Validate(enrollment.FinanceData.Representative);

                if (age < 18 && !representativeResults.IsValid)
                {
                    foreach (var error in representativeResults.Errors)
                    {
                        context.AddFailure("financeData.representative." + error.PropertyName, error.ErrorMessage);
                    }
                }
            });
            RuleFor(enrollment => enrollment.PersonalData).SetValidator(new PersonalDataValidator());
            RuleFor(enrollment => enrollment.FinanceData.Guarantors).SetCollectionValidator(new GuarantorValidator());
        }
    }
}
