using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Country : BaseModel
    {
        [Required]
        public string Name { get; set; }
    } 
}