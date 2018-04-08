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
    public class PersonalDataMessagesValidator : AbstractValidator<Models.PersonalData>
    {
        private List<PersonalDocumentType> _documentTypes { get; set; }

        public PersonalDataMessagesValidator(DatabaseContext databaseContext)
        {
            _documentTypes = databaseContext.Set<PersonalDocumentType>().ToList();

            RuleFor(personalData => personalData).Custom((personalData, context) =>
            {
                foreach (PersonalDocumentType documentType in _documentTypes)
                {
                    List<string> errors = Validate(personalData, documentType);

                    foreach (string error in errors)
                    {
                        context.AddFailure(string.Format("{0}", documentType.Id), string.Format("Documento {0} é obrigatório.", documentType.Name));
                    }
                }
            });

        }

        public List<string> Validate(Models.PersonalData personalData, DocumentType documentType)
        {
            List<string> errors = new List<string>();
            List<string> validations = documentType.Validations != null ? JsonConvert.DeserializeObject<List<string>>(documentType.Validations) : new List<string>();

            foreach (string validation in validations)
            {
                if (validation == DocumentValidations.Foreigner.ToString())
                {
                    if (!CheckForeign(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
                else if (validation == DocumentValidations.Native.ToString())
                {
                    if (!CheckNative(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
                else if (validation == DocumentValidations.MilitaryDraft.ToString())
                {
                    if (!CheckMilitaryDraft(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
                else if (validation == DocumentValidations.ForeignGraduation.ToString())
                {
                    if (!CheckForeignGraduation(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
                else if (validation == DocumentValidations.MinorAge.ToString())
                {
                    if (!CheckMinorAge(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
                else if (validation == DocumentValidations.GraduationYear.ToString())
                {
                    if (CheckGraduationYear(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
            }

            return errors;
        }

        private bool CheckForeign(Models.PersonalData personalData)
        {
            return (personalData.Nationality != null && personalData.Nationality.CheckForeign
                && personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).Validations.Contains(DocumentValidations.Foreigner.ToString())))
                || personalData.Nationality == null || !personalData.Nationality.CheckForeign;
        }

        private bool CheckNative(Models.PersonalData personalData)
        {
            return (personalData.Nationality != null && personalData.Nationality.CheckNative
                && personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).Validations.Contains(DocumentValidations.Native.ToString())))
                || personalData.Nationality == null || !personalData.Nationality.CheckNative;
        }

        private bool CheckMilitaryDraft(Models.PersonalData personalData)
        {
            return (personalData.Gender != null && personalData.Gender.CheckMilitaryDraft && personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).Validations.Contains(DocumentValidations.MilitaryDraft.ToString())))
            || personalData.Gender == null || !personalData.Gender.CheckMilitaryDraft;
        }

        private bool CheckForeignGraduation(Models.PersonalData personalData)
        {
            return (personalData.HighSchoolGraduationCountry != null && personalData.HighSchoolGraduationCountry.CheckForeignGraduation && personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).Validations.Contains(DocumentValidations.ForeignGraduation.ToString())))
                || personalData.HighSchoolGraduationCountry == null || !personalData.HighSchoolGraduationCountry.CheckForeignGraduation;
        }

        private bool CheckMinorAge(Models.PersonalData personalData)
        {
            return (personalData.BirthDate.HasValue && GetAge(personalData.BirthDate.Value) > 18 && personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).Validations.Contains(DocumentValidations.MinorAge.ToString())))
                || personalData.BirthDate.HasValue && GetAge(personalData.BirthDate.Value) > 18;
        }

        private bool CheckGraduationYear(Models.PersonalData personalData)
        {
            return (!string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) && personalData.HighSchoolGraduationYear == personalData.Enrollment.Onboarding.Year.ToString() && personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).Validations.Contains(DocumentValidations.GraduationYear.ToString()))) || string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) || personalData.HighSchoolGraduationYear != personalData.Enrollment.Onboarding.Year.ToString();
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

            foreach (Document document in documents.Where(x => x.DocumentTypeId.HasValue))
            {
                DocumentType documentType = _documentTypes.Single(x => x.Id == document.DocumentTypeId);

                if (!string.IsNullOrEmpty(documentType.Validations))
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
