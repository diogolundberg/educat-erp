using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class State
    {
        [Required]
        public string Name { get; set; }
    }   
}