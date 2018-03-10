using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class Country : BaseModel
    {
        public Country ()
        {
            OriginCountryEnrollments = new HashSet<Enrollment>();
            CountryOfGraduationFromHighSchoolEnrollments = new HashSet<Enrollment>();
        }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> OriginCountryEnrollments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> CountryOfGraduationFromHighSchoolEnrollments { get; set; }
    } 
}