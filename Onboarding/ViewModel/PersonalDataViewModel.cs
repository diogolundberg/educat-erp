using System;
using System.Collections.Generic;

namespace Onboarding.ViewModel
{
    public class PersonalDataViewModel
    {
        public string RealName { get; set; }

        public string AssumedName { get; set; }

        public DateTime BirthDate { get; set; }

        public string CPF { get; set; }

        public string Nationality { get; set; }

        public string HighSchoolGraduationYear { get; set; }

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
