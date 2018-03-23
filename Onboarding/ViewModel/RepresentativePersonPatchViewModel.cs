using Onboarding.Validations;

namespace Onboarding.ViewModel
{
    public class RepresentativePersonPatchViewModel : RepresentativePatchViewModel
    {
        [Cpf]
        public string Cpf { get; set; }

        public string Relationship { get; set; }
    }
}
