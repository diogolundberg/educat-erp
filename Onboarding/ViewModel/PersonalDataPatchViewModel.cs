using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Onboarding.Validations;

namespace Onboarding.ViewModel
{
    public class PersonalDataPatchViewModel
    {
        public string RealName { get; set; }

        public string AssumedName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Cpf]
        public string CPF { get; set; }

        public string Nationality { get; set; }

        public string HighSchoolGraduationYear { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Zipcode { get; set; }

        public string StreetAddress { get; set; }

        public string ComplementAddress { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneNumber { get; set; }

        public string Landline { get; set; }

        public string MothersName { get; set; }

        public string Handicap { get; set; }

        public string GenderId { get; set; }

        public string MaritalStatusId { get; set; }

        public string BirthCity { get; set; }

        public string BirthStateId { get; set; }

        public string BirthCountryId { get; set; }

        public string HighSchoolGraduationCountryId { get; set; }

        public string City { get; set; }

        public string StateId { get; set; }

        public string AddressKindId { get; set; }

        public string RaceId { get; set; }

        public string HighSchoolKindId { get; set; }

        public IEnumerable<string> Disabilities { get; set; }

        public IEnumerable<string> SpecialNeeds { get; set; }

        public IEnumerable<DocumentViewModel> Documents { get; set; }
    }
}
