using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class EnrollmentDisability
    {
       [Required]
        public Guid DisabilityId { get; set; }

        [JsonIgnore]
        [ForeignKey("DisabilityId")]
        public virtual Disability Disability { get; set; }

        [Required]
        public Guid EnrollmentId { get; set; }

        [JsonIgnore]
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment Enrollment { get; set; }
    }    
}        
 