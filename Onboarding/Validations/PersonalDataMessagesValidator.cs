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
    public class PersonalDataMessagesValidator : AbstractValidator<PersonalData>
    {
        private List<PersonalDocumentType> documentTypes { get; set; }

        public PersonalDataMessagesValidator(DatabaseContext databaseContext)
        {
            documentTypes = databaseContext.Set<PersonalDocumentType>().Where(x => x.Validations == null).ToList();

            RuleFor(personalData => personalData).Custom((personalData, context) =>
            {
                List<string> validations = new List<string>();
                validations.Add(BeForeign(personalData));
                validations.Add(BeMilitaryDraft(personalData));
                validations.Add(BeForeignGraduation(personalData));
                validations.Add(BeMinorAge(personalData));
                validations.Add(BeGraduationYear(personalData));
                validations = validations.Where(x => !string.IsNullOrEmpty(x)).ToList();

                List<Document> documents = personalData.PersonalDataDocuments.Select(x => x.Document).ToList();
                List<string> documentTypeValidations = GetPersonalDataDocumentValidations(documents);
                List<string> requiredDocumentValidations = validations.Where(x => !documentTypeValidations.Contains(x)).ToList();
                List<string> requiredDocumentTypes = documentTypes.Where(x => !documents.Any(o => o.DocumentTypeId == x.Id)).Select(x => x.Name).ToList();


                foreach (string requiredDocument in requiredDocumentValidations)
                {
                    context.AddFailure(GetMessageError(requiredDocument));
                }
                foreach (string documentType in requiredDocumentTypes)
                {
                    context.AddFailure(string.Format("Documento {0} é obrigatório.", documentType));
                }
            });

        }

        private string BeForeign(PersonalData personalData)
        {
            if (personalData.Nationality != null && personalData.Nationality.IsForeign)
            {
                return DocumentValidations.Foreigner.ToString();
            }
            else
            {
                return DocumentValidations.Native.ToString();
            }
        }

        private string BeMilitaryDraft(PersonalData personalData)
        {
            if (personalData.Gender != null && personalData.Gender.CheckMilitaryDraft)
            {
                return DocumentValidations.MilitaryDraft.ToString();
            }
            return string.Empty;
        }

        private string BeForeignGraduation(PersonalData personalData)
        {
            if (personalData.HighSchoolGraduationCountry != null && personalData.HighSchoolGraduationCountry.Name.ToLower() != "brasil")
            {
                return DocumentValidations.ForeignGraduation.ToString();
            }
            return string.Empty;
        }

        private string BeMinorAge(PersonalData personalData)
        {
            if (personalData.BirthDate.HasValue && GetAge(personalData.BirthDate.Value) > 18)
            {
                return DocumentValidations.MinorAge.ToString();
            }
            return string.Empty;
        }

        private string BeGraduationYear(PersonalData personalData)
        {
            if (!string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) && personalData.HighSchoolGraduationYear == personalData.Enrollment.Deadline.Year.ToString())
            {
                return DocumentValidations.GraduationYear.ToString();
            }
            return string.Empty;
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

        private List<String> GetPersonalDataDocumentValidations(List<Document> documents)
        {
            List<string> documentTypeValidations = new List<string>();

            foreach (Document document in documents)
            {
                if (!string.IsNullOrEmpty(document.DocumentType.Validations))
                {
                    List<string> documentValidations = JsonConvert.DeserializeObject<List<string>>(document.DocumentType.Validations);
                    documentTypeValidations.AddRange(documentValidations.Where(x => !documentTypeValidations.Contains(x)));
                }
            }

            return documentTypeValidations;
        }

        private string GetMessageError(string validation)
        {
            if (validation == DocumentValidations.Foreigner.ToString())
            {
                return "Documento RNE é obrigatório.";
            }
            else if (validation == DocumentValidations.MilitaryDraft.ToString())
            {
                return "Documento Militar é obrigatório.";
            }
            else if (validation == DocumentValidations.ForeignGraduation.ToString())
            {
                return "Parecer da secretaria de educação e publicação no diário oficial é obrigatório.";
            }
            else if (validation == DocumentValidations.MinorAge.ToString())
            {
                return "Título de Eleitor e Comprovante de Votação é obrigatório.";

            }
            else if (validation == DocumentValidations.GraduationYear.ToString())
            {
                return "Declaração de conclusão do ensino médio ou histórico escolar é obrigatório.";
            }
            else if (validation == DocumentValidations.Native.ToString())
            {
                return "CPF é obrigatório.";
            }

            return string.Empty;
        }
    }
}
