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

        public string BirthDate { get; set; }

        public string CPF { get; set; }

        public string HighSchoolGraduationYear { get; set; }

        public string Email { get; set; }

        public string Zipcode { get; set; }

        public string StreetAddress { get; set; }

        public string ComplementAddress { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneNumber { get; set; }

        public string Landline { get; set; }

        public string MothersName { get; set; }

        internal bool Any(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        public string Handicap { get; set; }

        [ForeignKey("Gender")]
        public int? GenderId { get; set; }

        [JsonIgnore]
        public virtual Gender Gender { get; set; }

        [ForeignKey("MaritalStatus")]
        public int? MaritalStatusId { get; set; }

        [JsonIgnore]
        public virtual MaritalStatus MaritalStatus { get; set; }

        [ForeignKey("BirthCity")]
        public int? BirthCityId { get; set; }

        [JsonIgnore]
        public City BirthCity { get; set; }

        [ForeignKey("BirthState")]
        public int? BirthStateId { get; set; }

        [JsonIgnore]
        public virtual State BirthState { get; set; }

        [ForeignKey("BirthCountry")]
        public int? BirthCountryId { get; set; }

        [JsonIgnore]
        public virtual Country BirthCountry { get; set; }

        [ForeignKey("HighSchoolGraduationCountry")]
        public int? HighSchoolGraduationCountryId { get; set; }

        [JsonIgnore]
        public virtual Country HighSchoolGraduationCountry { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        [JsonIgnore]
        public virtual City City { get; set; }

        [ForeignKey("State")]
        public int? StateId { get; set; }

        [JsonIgnore]
        public virtual State State { get; set; }

        [ForeignKey("AddressKind")]
        public int? AddressKindId { get; set; }

        [JsonIgnore]
        public virtual AddressKind AddressKind { get; set; }

        [ForeignKey("Race")]
        public int? RaceId { get; set; }

        [JsonIgnore]
        public virtual Race Race { get; set; }

        [ForeignKey("HighSchoolKind")]
        public int? HighSchoolKindId { get; set; }

        [JsonIgnore]
        public virtual HighSchoolKind HighSchollKind { get; set; }

        [ForeignKey("Nationality")]
        public int? NationalityId { get; set; }

        [JsonIgnore]
        public virtual Nationality Nationality { get; set; }

        [ForeignKey("Enrollment")]
        public int? EnrollmentId { get; set; }

        [JsonIgnore]
        public virtual Enrollment Enrollment { get; set; }

        public virtual ICollection<PersonalDataSpecialNeed> PersonalDataSpecialNeeds { get; set; }

        public virtual ICollection<PersonalDataDisability> PersonalDataDisabilities { get; set; }

        public virtual ICollection<PersonalDataDocument> PersonalDataDocuments { get; set; }
    }
}
