using System.ComponentModel.DataAnnotations;
using Onboarding.Validations;

namespace Onboarding.ViewModel
{
    public class RepresentativePersonViewModel : RepresentativeViewModel
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Relationship { get; set; }
    }
}
