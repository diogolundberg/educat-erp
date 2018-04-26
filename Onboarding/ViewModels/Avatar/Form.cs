using System.ComponentModel.DataAnnotations;

namespace onboarding.ViewModels.Avatar
{
    public class Form
    {
        [Required]
        public string photo { get; set; }
    }
}
