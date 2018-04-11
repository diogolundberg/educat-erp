using FluentValidation;
using FluentValidation.Validators;
using Newtonsoft.Json;
using Onboarding.Enums;
using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Validations
{
    public class FinanceDataMessagesValidator : AbstractValidator<Models.FinanceData>
    {
        public FinanceDataMessagesValidator(DatabaseContext databaseContext)
        {
            RuleFor(financeData => financeData).Custom((financeData, context) =>
            {
                databaseContext.Entry(financeData).Reference(x => x.Plan).Load();
                databaseContext.Entry(financeData).Collection(x => x.Guarantors).Load();
                databaseContext.Entry(financeData).Reference(x => x.Representative).Load();

                CheckPlan(financeData, context);
                CheckRepresentative(financeData, context);
            });
        }

        private void CheckPlan(Models.FinanceData financeData, CustomContext context)
        {
            if (financeData.Plan != null && financeData.Plan.Guarantors > 0)
            {
                if (financeData.Plan.Guarantors > financeData.Guarantors.Count)
                {
                    context.AddFailure(string.Format("Para o plano {0} é necessário preencher {1} fiador(es)", financeData.Plan.Name, financeData.Plan.Guarantors));
                }
            }
        }

        private void CheckRepresentative(Models.FinanceData financeData, CustomContext context)
        {
            Models.PersonalData personalData = financeData.Enrollment.PersonalData;
            Representative representative = financeData.Representative;

            if (!string.IsNullOrEmpty(personalData.BirthDate) && GetAge(personalData.BirthDate) > 18)
            {
                bool isDiff = false;

                if (representative is RepresentativePerson)
                {
                    isDiff = representative.Name != personalData.RealName ? true : isDiff;
                    isDiff = representative.StreetAddress != personalData.StreetAddress ? true : isDiff;
                    isDiff = representative.Neighborhood != personalData.Neighborhood ? true : isDiff;
                    isDiff = representative.PhoneNumber != personalData.PhoneNumber ? true : isDiff;
                    isDiff = representative.Landline != personalData.Landline ? true : isDiff;
                    isDiff = representative.Email != personalData.Email ? true : isDiff;
                    isDiff = representative.CityId != personalData.CityId ? true : isDiff;
                    isDiff = representative.StateId != personalData.StateId ? true : isDiff;
                    isDiff = ((RepresentativePerson)representative).Cpf != personalData.CPF ? true : isDiff;
                }

                if (isDiff)
                {
                    context.AddFailure("Dados do responsável não são os mesmos dos dados pessoais.");
                }
            }
        }

        private int GetAge(string birthDate)
        {
            DateTime today = DateTime.Today;
            DateTime.TryParse(birthDate, out DateTime birthDateConverted);

            int age = today.Year - birthDateConverted.Year;

            if (birthDateConverted > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
