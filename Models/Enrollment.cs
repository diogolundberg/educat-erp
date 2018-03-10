using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class Enrollment : BaseModel
    {
        public Enrollment ()
        {
            EnrollmentDisabilities = new HashSet<EnrollmentDisability>();
        }

        public string Name { get; set; }

        public string SocialName { get; set; }

        public string Cpf { get; set; }

        public DateTime Birthday { get; set; }

        public Guid? CivilStatusId { get; set; }

        [JsonIgnore]
        [ForeignKey("CivilStatusId")]
        public virtual CivilStatus CivilStatus { get; set; }

        public Guid? GenderId { get; set; }

        [JsonIgnore]
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        public Guid? NationalityId { get; set; }

        [JsonIgnore]
        [ForeignKey("NationalityId")]
        public virtual Nationality Nationality { get; set; }   

        public Guid? OriginCountryId { get; set; }

        [JsonIgnore]
        [ForeignKey("OriginCountryId")]
        public virtual Country OriginCountry { get; set; }

        public Guid? BirthStateId { get; set; }

        [JsonIgnore]
        [ForeignKey("BirthStateId")]
        public virtual State BornState { get; set; }

        public string YearofHighSchoolGraduation { get; set; }

        public Guid? CountryOfGraduationFromHighSchoolId { get; set; }

        [JsonIgnore]
        [ForeignKey("CountryOfGraduationFromHighSchoolId")]
        public virtual Country CountryOfGraduationFromHighSchool { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Guid? PhoneTypeId { get; set; }

        [JsonIgnore]
        [ForeignKey("PhoneTypeId")]
        public virtual PhoneType PhoneType { get; set; }

        public string PhoneNumber { get; set; }

        public string Cep { get; set; }

        public string Address { get; set; }

        public string Number { get; set; }

        public string Neighborhood { get; set;  }

        public string City { get; set; }

        public Guid? CountryStateId { get; set; }

        [JsonIgnore]
        [ForeignKey("CountryStateId")]
        public virtual State CountryState { get; set; }

        public Guid? AddressTypeId { get; set; }

        [JsonIgnore]
        [ForeignKey("AddressTypeId")]
        public virtual AddressType  AddressType { get; set; }

        public Guid? RaceId { get; set; }

        [JsonIgnore]
        [ForeignKey("RaceId")]
        public virtual Race Race { get; set; }

        public Guid? SchoolId { get; set; }

        [JsonIgnore]
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; } 

        public string MothersName { get; set; }

        public DateTimeOffset? SendBy { get; set; }

        public bool? HasHandicaps { get; set; }

        public string BirthCity { get; set; }

        public virtual ICollection<EnrollmentDisability> EnrollmentDisabilities { get; set; }
    }
}