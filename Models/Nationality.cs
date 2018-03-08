using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Nationality : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }    
}