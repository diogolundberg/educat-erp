using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModels.Avatar
{
    public class Form
    {
        [Required]
        public string photo { get; set; }
    }
}
