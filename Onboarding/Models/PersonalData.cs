using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class PersonalData : BaseModel
    {
        public PersonalData()
        {
            PersonalDataSpecialNeeds = new HashSet<PersonalDataSpecialNeed>();
            PersonalDataDisabilities = new HashSet<PersonalDataDisability>();
            PersonalDataDocuments = new HashSet<PersonalDataDocument>();
        }

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

        [JsonIgnore]
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        public Guid? MaritalStatusId { get; set; }

        [JsonIgnore]
        [ForeignKey("MaritalStatusId")]
        public virtual MaritalStatus MaritalStatus { get; set; }

        public string BirthCity { get; set; }

        public Guid? BirthStateId { get; set; }

        [JsonIgnore]
        [ForeignKey("BirthStateId")]
        public virtual State BirthState { get; set; }

        public Guid? BirthCountryId { get; set; }

        [JsonIgnore]
        [ForeignKey("BirthCountryId")]
        public virtual Country BirthCountry { get; set; }

        public Guid? HighSchoolGraduationCountryId { get; set; }

        [JsonIgnore]
        [ForeignKey("HighSchoolGraduationCountryId")]
        public virtual Country HighSchoolGraduationCountry { get; set; }

        public string City { get; set; }

        public Guid? StateId { get; set; }

        [JsonIgnore]
        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        public Guid? AddressKindId { get; set; }

        [JsonIgnore]
        [ForeignKey("AddressKindId")]
        public virtual AddressKind AddressKind { get; set; }

        public Guid? RaceId { get; set; }

        [JsonIgnore]
        [ForeignKey("RaceId")]
        public virtual Race Race { get; set; }

        public Guid? HighSchoolKindId { get; set; }

        [JsonIgnore]
        [ForeignKey("HighSchoolKindId")]
        public virtual HighSchoolKind HighSchollKind { get; set; }


        public Guid EnrollmentId { get; set; }

        [JsonIgnore]
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment Enrollment { get; set; }

        public virtual ICollection<PersonalDataSpecialNeed> PersonalDataSpecialNeeds { get; set; }

        public virtual ICollection<PersonalDataDisability> PersonalDataDisabilities { get; set; }

        public virtual ICollection<PersonalDataDocument> PersonalDataDocuments { get; set; }
    }
}