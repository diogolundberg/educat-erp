using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Email : BaseModel
    {
        [Required]
        public string Value { get; set; }
    } 
}