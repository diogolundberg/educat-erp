using System;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public class EnrollmentViewModel 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SocialName { get; set; }

        public int Cpf { get; set; }

        public DateTime Birthday { get; set; }

        public Guid? CivilStatusId { get; set; }

        public Guid? GenderId { get; set; }

        public Guid? NationalityId { get; set; }

        public Guid? OriginCountryId { get; set; }

        public Guid? BornStateId { get; set; }

        public int? YearofHighSchoolGraduation { get; set; }

        public Guid? CountryOfGraduationFromHighSchoolId { get; set; }

        public string Email { get; set; }

        public Guid? PhoneTypeId { get; set; }

        public string PhoneNumber { get; set; }

        public string Cep { get; set; }

        public string Address { get; set; }

        public int? Number { get; set; }

        public string Neighborhood { get; set;  }

        public string City { get; set; }

        public Guid? CountryStateId { get; set; }

        public Guid? AddressTypeId { get; set; }

        public Guid? RaceId { get; set; }

        public Guid? SchoolId { get; set; }

        public string MotherMom { get; set; }

        public DateTimeOffset? SendBy { get; set; }

        public bool? HaveHandicaps { get; set; }
    }
}