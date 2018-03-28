using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Onboarding.Validations;

namespace Onboarding.ViewModel
{
    public class PersonalDataViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public int? EnrollmentId { get; set; }

        public string State { get; set; }

        public string RealName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }

        public string AssumedName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string HighSchoolGraduationYear { get; set; }


        [Required]
        public string Zipcode { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string ComplementAddress { get; set; }

        [Required]
        public string Neighborhood { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Landline { get; set; }

        [Required]
        public string MothersName { get; set; }

        [Required]
        public string Handicap { get; set; }

        [Required]
        public int? GenderId { get; set; }

        [Required]
        public int? MaritalStatusId { get; set; }

        [Required]
        public int? BirthCityId { get; set; }

        [Required]
        public int? BirthStateId { get; set; }

        [Required]
        public int? BirthCountryId { get; set; }

        [Required]
        public int? HighSchoolGraduationCountryId { get; set; }

        [Required]
        public int? CityId { get; set; }

        [Required]
        public int? StateId { get; set; }

        [Required]
        public int? AddressKindId { get; set; }

        [Required]
        public int? RaceId { get; set; }

        [Required]
        public int? HighSchoolKindId { get; set; }

        public IEnumerable<string> Disabilities { get; set; }

        public IEnumerable<string> SpecialNeeds { get; set; }

        [Required]
        public IEnumerable<DocumentViewModel> Documents { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
