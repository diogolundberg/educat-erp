using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class Enrollment
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int AddressTypeId { get; set; }

        [ForeignKey("AddressTypeId")]
        public virtual AddressType  AddressType { get; set; }

        [Required]
        public int CivilStatusId { get; set; }

        [ForeignKey("CivilStatusId")]
        public virtual CivilStatus CivilStatus { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        [Required]
        public int GenderId { get; set; }

        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [Required]
        public int NationalityId { get; set; }

        [ForeignKey("Nationality")]
        public virtual Nationality Nationality { get; set; }

        [Required]
        public int PhoneTypeId { get; set; }

        [ForeignKey("PhoneTypeId")]
        public virtual PhoneType PhoneType { get; set; }

        [Required]
        public int RaceId { get; set; }

        [ForeignKey("RaceId")]
        public virtual Race Race { get; set; }

        [Required]
        public int SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public virtual School School { get; set; } 

        [Required]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }
    }
}