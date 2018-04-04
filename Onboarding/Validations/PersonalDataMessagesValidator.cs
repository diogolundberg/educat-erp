using FluentValidation;
using Onboarding.Enums;
using Onboarding.Models;
using System;
using System.Linq;

namespace Onboarding.Validations
{
    public class PersonalDataMessagesValidator : AbstractValidator<PersonalData>
    {
        public PersonalDataMessagesValidator()
        {
            RuleFor(personalData => personalData)
                .Must(BeForeign)
                .When(x => x.Nationality != null && x.Nationality.IsForeign)
                .WithMessage("Documento RNE é obrigatório.");

            RuleFor(personalData => personalData)
                .Must(BeMilitaryDraft)
                .When(x => x.Gender != null && x.Gender.CheckMilitaryDraft)
                .WithMessage("Documento Militar é obrigatório.");

            RuleFor(personalData => personalData)
                .Must(BeForeignGraduation)
                .When(x => x.HighSchoolGraduationCountry != null && x.HighSchoolGraduationCountry.Name.ToLower() != "brasil")
                .WithMessage("Parecer da secretaria de educação e publicação no diário oficial é obrigatório.");

            RuleFor(personalData => personalData)
                .Must(BeMinorAge)
                .When(x => x.BirthDate.HasValue && GetAge(x.BirthDate.Value) > 18)
                .WithMessage("Título de Eleitor e Comprovante de Votação é obrigatório.");
        }

        private bool BeForeign(PersonalData personalData)
        {
            if (personalData.PersonalDataDocuments.Any(x => x.Document.DocumentType.Validations.Contains(DocumentValidations.Foreigner.ToString())))
            {
                return true;
            }

            return false;
        }

        private bool BeMilitaryDraft(PersonalData personalData)
        {
            if (personalData.PersonalDataDocuments.Any(x => x.Document.DocumentType.Validations.Contains(DocumentValidations.MilitaryDraft.ToString())))
            {
                return true;
            }

            return false;
        }

        private bool BeForeignGraduation(PersonalData personalData)
        {
            if (personalData.PersonalDataDocuments.Any(x => x.Document.DocumentType.Validations.Contains(DocumentValidations.ForeignGraduation.ToString())))
            {
                return true;
            }

            return false;
        }

        private bool BeMinorAge(PersonalData personalData)
        {
            if (personalData.PersonalDataDocuments.Any(x => x.Document.DocumentType.Validations.Contains(DocumentValidations.MinorAge.ToString())))
            {
                return true;
            }

            return false;
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
