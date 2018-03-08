using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class PhoneType : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }    
}