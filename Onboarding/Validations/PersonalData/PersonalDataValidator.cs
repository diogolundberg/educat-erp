using FluentValidation;
using Onboarding.Models;
using System;

namespace Onboarding.Validations.PersonalData
{
    public class PersonalDataValidator : AbstractValidator<Models.PersonalData>
    {
        public PersonalDataValidator()
        {
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
            RuleFor(personalData => personalData.PersonalDataDocuments).SetCollectionValidator(x => new PersonalDataDocumentValidator());
        }
    }
}
