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

        public Guid? GenderId { get; set; }

        public Guid? MaritalStatusId { get; set; }

        public string BirthCity { get; set; }

        public Guid? BirthStateId { get; set; }

        public Guid? BirthCountryId { get; set; }

        public Guid? HighSchoolGraduationCountryId { get; set; }

        public string City { get; set; }

        public Guid? StateId { get; set; }

        public Guid? AddressKindId { get; set; }

        public Guid? RaceId { get; set; }

        public Guid? HighSchoolKindId { get; set; }

        public IEnumerable<Guid> Disabilities { get; set; }

        public IEnumerable<Guid> SpecialNeeds { get; set; }

        public IEnumerable<string> Documents { get; set; }
    }
}
