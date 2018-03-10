using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public virtual ICollection<Enrollment> OriginCountryEnrollments { get; set; }

        public virtual ICollection<Enrollment> CountryOfGraduationFromHighSchoolEnrollments { get; set; }
    } 
}