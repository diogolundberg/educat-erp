using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Race : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }    
}