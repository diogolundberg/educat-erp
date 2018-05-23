using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace onboarding.Models
{
    public class Race : BaseModel
    {
        public Race ()
        {
        }
        
        [Required]
        public string Name { get; set; }
    }    
}