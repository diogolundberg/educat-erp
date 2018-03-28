using Onboarding.Validations;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public class RepresentativeViewModel
    {
        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string ComplementAddress { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneNumber { get; set; }

        public string Landline { get; set; }

        public string Email { get; set; }

        public int? CityId { get; set; }

        public int? StateId { get; set; }
    }

    public class RepresentativeCompanyViewModel : RepresentativeViewModel
    {
        public string Cnpj { get; set; }

        public string Contact { get; set; }
    }

    public class RepresentativePersonViewModel : RepresentativeViewModel
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Relationship { get; set; }
    }
}
