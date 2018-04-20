namespace Onboarding.ViewModels.FinanceApprovals
{
    public class Representative
    {
        public string Discriminator { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Landline { get; set; }

        public string Zipcode { get; set; }
        public string StreetAddress { get; set; }
        public string AddressNumber { get; set; }
        public string ComplementAddress { get; set; }
        public string Neighborhood { get; set; }
        public string AddressKind { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class RepresentativeCompany : Representative
    {
        public string Cnpj { get; set; }
        public string Contact { get; set; }
    }

    public class RepresentativePerson : Representative
    {
        public string Cpf { get; set; }
        public string Relationship { get; set; }
    }
}
