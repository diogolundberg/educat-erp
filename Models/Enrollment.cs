using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int Cpf { get; set; }

        public DateTime Birthday { get; set; }

        public Guid? CivilStatusId { get; set; }

        [ForeignKey("CivilStatusId")]
        public virtual CivilStatus CivilStatus { get; set; }

        public Guid? GenderId { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        public Guid? NationalityId { get; set; }

        [ForeignKey("NationalityId")]
        public virtual Nationality Nationality { get; set; }   

        public Guid? OriginCountryId { get; set; }

        [ForeignKey("OriginCountryId")]
        public virtual Country OriginCountry { get; set; }

        public Guid? BornStateId { get; set; }

        [ForeignKey("BornStateId")]
        public virtual State BornState { get; set; }

        public int? YearofHighSchoolGraduation { get; set; }

        public Guid? CountryOfGraduationFromHighSchoolId { get; set; }

        [ForeignKey("CountryOfGraduationFromHighSchoolId")]
        public virtual Country CountryOfGraduationFromHighSchool { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Guid? PhoneTypeId { get; set; }

        [ForeignKey("PhoneTypeId")]
        public virtual PhoneType PhoneType { get; set; }

        public string PhoneNumber { get; set; }

        public string Cep { get; set; }

        public string Address { get; set; }

        public int? Number { get; set; }

        public string Neighborhood { get; set;  }

        public string City { get; set; }

        public Guid? CountryStateId { get; set; }

        [ForeignKey("CountryStateId")]
        public virtual State CountryState { get; set; }

        public Guid? AddressTypeId { get; set; }

        [ForeignKey("AddressTypeId")]
        public virtual AddressType  AddressType { get; set; }

        public Guid? RaceId { get; set; }

        [ForeignKey("RaceId")]
        public virtual Race Race { get; set; }

        public Guid? SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public virtual School School { get; set; } 

        public string MotherMom { get; set; }

        public DateTimeOffset? SendBy { get; set; }

        public bool? HaveHandcaps { get; set; }

        public virtual ICollection<EnrollmentDisability> EnrollmentDisabilities { get; set; }
    }
}