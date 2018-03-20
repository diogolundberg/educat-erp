using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
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