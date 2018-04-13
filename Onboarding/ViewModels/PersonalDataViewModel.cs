using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Onboarding.ViewModels
{
    public class PersonalDataViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public int? EnrollmentId { get; set; }

        public string Status { get; set; }

        public string RealName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string AssumedName { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public int? NationalityId { get; set; }

        [Required]
        public string HighSchoolGraduationYear { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string AddressNumber { get; set; }

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

        public string BirthCityName { get; set; }

        public string BirthStateName { get; set; }

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

        public IEnumerable<int> Disabilities { get; set; }

        public IEnumerable<int> SpecialNeeds { get; set; }

        [Required]
        public IEnumerable<DocumentViewModel> Documents { get; set; }

        [JsonIgnore]
        public string UpdatedAt { get; set; }

        public bool Editable { get; set; }
    }
}
