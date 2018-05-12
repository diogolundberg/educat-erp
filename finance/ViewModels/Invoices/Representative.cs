using finance.Validations;
using System.ComponentModel.DataAnnotations;

namespace finance.ViewModels.Invoices
{
    public class Representative
    {
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
        public string AddressKind { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Discriminator { get; set; }
    }

    public class RepresentativeCompany : Representative
    {
        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string Contact { get; set; }
    }

    public class RepresentativePerson : Representative
    {
        [Cpf]
        [Required]
        public string Cpf { get; set; }
    }
}