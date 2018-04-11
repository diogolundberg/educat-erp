using Onboarding.Validations;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModels
{
    public class RepresentativeViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string AddressNumber { get; set; }

        [Required]
        public string ComplementAddress { get; set; }

        [Required]
        public string Neighborhood { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Landline { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public int? AddressKindId { get; set; }

        [Required]
        public int? CityId { get; set; }

        [Required]
        public int? StateId { get; set; }

        [Required]
        public string Discriminator { get; set; }
    }

    public class RepresentativeCompanyViewModel : RepresentativeViewModel
    {
        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class RepresentativePersonViewModel : RepresentativeViewModel
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }

        [Required]
        public int? RelationshipId { get; set; }
    }
}
