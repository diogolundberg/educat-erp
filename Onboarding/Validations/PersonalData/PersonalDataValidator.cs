using FluentValidation;
using Newtonsoft.Json;
using Onboarding.Enums;
using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Validations.PersonalData
{
    public class PersonalDataValidator : AbstractValidator<Models.PersonalData>
    {
        private List<PersonalDocumentType> _documentTypes { get; set; }

        public PersonalDataValidator(DatabaseContext databaseContext)
        {
            _documentTypes = databaseContext.Set<PersonalDocumentType>().ToList();

            RuleFor(personalData => personalData.RealName).NotEmpty();
            RuleFor(personalData => personalData.AssumedName).NotEmpty();
            RuleFor(personalData => personalData.BirthDate).NotEmpty();
            RuleFor(personalData => personalData.CPF).NotEmpty().Must(cpf => Cpf.ValidCPF(cpf));
            RuleFor(personalData => personalData.NationalityId).NotEmpty();
            RuleFor(personalData => personalData.HighSchoolGraduationYear).NotEmpty();
            RuleFor(personalData => personalData.Email).NotEmpty().EmailAddress();
            RuleFor(personalData => personalData.Zipcode).NotEmpty();
            RuleFor(personalData => personalData.StreetAddress).NotEmpty();
            RuleFor(personalData => personalData.ComplementAddress).NotEmpty();
            RuleFor(personalData => personalData.Neighborhood).NotEmpty();
            RuleFor(personalData => personalData.PhoneNumber).NotEmpty();
            RuleFor(personalData => personalData.Landline).NotEmpty();
            RuleFor(personalData => personalData.MothersName).NotEmpty();
            RuleFor(personalData => personalData.Handicap).NotEmpty();
            RuleFor(personalData => personalData.GenderId).NotEmpty();
            RuleFor(personalData => personalData.MaritalStatusId).NotEmpty();
            RuleFor(personalData => personalData.BirthCityId).NotEmpty();
            RuleFor(personalData => personalData.BirthStateId).NotEmpty();
            RuleFor(personalData => personalData.BirthCountryId).NotEmpty();
            RuleFor(personalData => personalData.HighSchoolGraduationCountryId).NotEmpty();
            RuleFor(personalData => personalData.CityId).NotEmpty();
            RuleFor(personalData => personalData.StateId).NotEmpty();
            RuleFor(personalData => personalData.AddressKindId).NotEmpty();
            RuleFor(personalData => personalData.RaceId).NotEmpty();
            RuleFor(personalData => personalData.HighSchoolKindId).NotEmpty();
            RuleFor(personalData => personalData.PersonalDataDocuments).SetCollectionValidator(x => new PersonalDataDocumentValidator()).OverridePropertyName("documents");
            RuleFor(personalData => personalData).Custom((personalData, context) =>
            {
                foreach (PersonalDocumentType documentType in _documentTypes)
                {
                    List<string> errors = Validate(personalData, documentType);

                    foreach (string error in errors)
                    {
                        context.AddFailure(string.Format("documents.{0}", documentType.Id), error);
                    }
                }
            });
        }

        public List<string> Validate(Models.PersonalData personalData, DocumentType documentType)
        {
            List<string> errors = new List<string>();
            List<string> validations = documentType.Validations != null ? JsonConvert.DeserializeObject<List<string>>(documentType.Validations) : new List<string>();

            if (documentType.Validations == null)
            {
                if (!personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId == documentType.Id))
                {
                    errors.Add(string.Format("Documento {0} é obrigatório.", documentType.Name));
                }
            }

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
                    if (!CheckGraduationYear(personalData))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
            }

            return errors;
        }

        private bool CheckForeign(Models.PersonalData personalData)
        {
            if (personalData.Nationality == null || !personalData.Nationality.CheckForeign)
            {
                return true;
            }
            else
            {
                return (personalData.Nationality != null && personalData.Nationality.CheckForeign && HasValidation(personalData, DocumentValidations.Foreigner.ToString()));
            }
        }

        private bool CheckNative(Models.PersonalData personalData)
        {
            if (personalData.Nationality == null || !personalData.Nationality.CheckNative)
            {
                return true;
            }
            else
            {
                return (personalData.Nationality != null && personalData.Nationality.CheckNative && HasValidation(personalData, DocumentValidations.Native.ToString()));
            }
        }

        private bool CheckMilitaryDraft(Models.PersonalData personalData)
        {
            if (personalData.Gender == null || !personalData.Gender.CheckMilitaryDraft)
            {
                return true;
            }
            else
            {
                return (personalData.Gender != null && personalData.Gender.CheckMilitaryDraft && HasValidation(personalData, DocumentValidations.MilitaryDraft.ToString()));
            }
        }

        private bool CheckForeignGraduation(Models.PersonalData personalData)
        {
            if (personalData.HighSchoolGraduationCountry == null || !personalData.HighSchoolGraduationCountry.CheckForeignGraduation)
            {
                return true;
            }
            else
            {
                return (personalData.HighSchoolGraduationCountry != null && personalData.HighSchoolGraduationCountry.CheckForeignGraduation && HasValidation(personalData, DocumentValidations.ForeignGraduation.ToString()));
            }
        }

        private bool CheckMinorAge(Models.PersonalData personalData)
        {
            if (!personalData.BirthDate.HasValue || GetAge(personalData.BirthDate.Value) < 18)
            {
                return true;
            }
            else
            {
                return (personalData.BirthDate.HasValue && GetAge(personalData.BirthDate.Value) >= 18 && HasValidation(personalData, DocumentValidations.MinorAge.ToString()));
            }
        }

        private bool CheckGraduationYear(Models.PersonalData personalData)
        {
            if (string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) || personalData.HighSchoolGraduationYear != personalData.Enrollment.Onboarding.Year.ToString())
            {
                return true;
            }
            else
            {
                return (!string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) && personalData.HighSchoolGraduationYear == personalData.Enrollment.Onboarding.Year.ToString() && HasValidation(personalData, DocumentValidations.GraduationYear.ToString()));
            }
        }

        private bool HasValidation(Models.PersonalData personalData, string validation)
        {
            return personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).ValidationList.Contains(validation));
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
