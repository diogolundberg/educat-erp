using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public class HighSchoolKind : BaseModel
    {
        public HighSchoolKind() 
        {
        }

        [Required]
        public string Name { get; set; }
    }   
}