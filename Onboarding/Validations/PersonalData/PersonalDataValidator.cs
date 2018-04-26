using FluentValidation;
using Newtonsoft.Json;
using onboarding.Enums;
using onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using onboarding.Resources.Models;

namespace onboarding.Validations.PersonalData
{
    public class PersonalDataValidator : AbstractValidator<Models.PersonalData>
    {
        private List<PersonalDocumentType> _documentTypes { get; set; }

        public PersonalDataValidator(DatabaseContext databaseContext)
        {
            _documentTypes = databaseContext.Set<PersonalDocumentType>().ToList();

            RuleFor(personalData => personalData.RealName).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.RealName);
            RuleFor(personalData => personalData.AssumedName).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.AssumedName);
            RuleFor(personalData => personalData.BirthDate).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.BirthDate);
            RuleFor(personalData => personalData.NationalityId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.NationalityId);
            RuleFor(personalData => personalData.HighSchoolGraduationYear).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.HighSchoolGraduationYear);
            RuleFor(personalData => personalData.Zipcode).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.Zipcode);
            RuleFor(personalData => personalData.StreetAddress).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.StreetAddress);
            RuleFor(personalData => personalData.AddressNumber).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.AddressNumber);
            RuleFor(personalData => personalData.Neighborhood).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.Neighborhood);
            RuleFor(personalData => personalData.PhoneNumber).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.PhoneNumber);
            RuleFor(personalData => personalData.Landline).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.Landline);
            RuleFor(personalData => personalData.MothersName).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.MothersName);
            RuleFor(personalData => personalData.Handicap).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.Handicap);
            RuleFor(personalData => personalData.GenderId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.GenderId);
            RuleFor(personalData => personalData.MaritalStatusId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.MaritalStatusId);
            RuleFor(personalData => personalData.BirthCityId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.BirthCityId);
            RuleFor(personalData => personalData.BirthCountryId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.BirthCountryId);
            RuleFor(personalData => personalData.HighSchoolGraduationCountryId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.HighSchoolGraduationCountryId);
            RuleFor(personalData => personalData.CityId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.CityId);
            RuleFor(personalData => personalData.StateId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.StateId);
            RuleFor(personalData => personalData.AddressKindId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.AddressKindId);
            RuleFor(personalData => personalData.RaceId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.RaceId);
            RuleFor(personalData => personalData.HighSchoolKindId).NotEmpty().WithName(onboarding.Resources.Models.PersonalData.HighSchoolKindId);

            RuleFor(personalData => personalData.Email).NotEmpty();
            RuleFor(personalData => personalData.CPF).NotEmpty().Must(cpf => Cpf.ValidCPF(cpf));
            RuleFor(personalData => personalData.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(personalData => personalData).Custom((personalData, context) =>
            {
                databaseContext.Entry(personalData).Reference(x => x.BirthCountry).Load();

                if (personalData.BirthCountry != null)
                {
                    if (personalData.BirthCountry.HasUF && personalData.BirthStateId == null)
                    {
                        context.AddFailure("BirthStateId", "UF de nascimento é obrigatório.");
                    }
                }
            });
            RuleFor(personalData => personalData.PersonalDataDocuments).SetCollectionValidator(x => new PersonalDataDocumentValidator()).WithName("documents");
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
                    errors.Add(string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, documentType.Name));
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
                else if (validation == DocumentValidations.NotGraduationYear.ToString())
                {
                    if (!CheckNotGraduationYear(personalData))
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
            if (string.IsNullOrEmpty(personalData.BirthDate) || GetAge(personalData.BirthDate) < 18)
            {
                return true;
            }
            else
            {
                return (!string.IsNullOrEmpty(personalData.BirthDate) && GetAge(personalData.BirthDate) >= 18 && HasValidation(personalData, DocumentValidations.MinorAge.ToString()));
            }
        }

        private bool CheckGraduationYear(Models.PersonalData personalData)
        {
            if (string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) || personalData.HighSchoolGraduationYear != personalData.Enrollment.Onboarding.Year)
            {
                return true;
            }
            else
            {
                return (HasValidation(personalData, DocumentValidations.GraduationYear.ToString()));
            }
        }

        private bool CheckNotGraduationYear(Models.PersonalData personalData)
        {
            if (string.IsNullOrEmpty(personalData.HighSchoolGraduationYear) || personalData.HighSchoolGraduationYear == personalData.Enrollment.Onboarding.Year)
            {
                return true;
            }
            else
            {
                return (HasValidation(personalData, DocumentValidations.NotGraduationYear.ToString()));
            }
        }


        private bool HasValidation(Models.PersonalData personalData, string validation)
        {
            return personalData.PersonalDataDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).ValidationList.Contains(validation));
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

        private string GetMessageError(string validation)
        {
            if (validation == DocumentValidations.Foreigner.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.Foreigner);
            }
            else if (validation == DocumentValidations.MilitaryDraft.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.MilitaryDraft);
            }
            else if (validation == DocumentValidations.ForeignGraduation.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.ForeignGraduation);
            }
            else if (validation == DocumentValidations.MinorAge.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.MinorAge);
            }
            else if (validation == DocumentValidations.GraduationYear.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.GraduationYear);
            }
            else if (validation == DocumentValidations.NotGraduationYear.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.NotGraduationYear);
            }
            else if (validation == DocumentValidations.Native.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.Native);
            }

            return string.Empty;
        }
    }
}
