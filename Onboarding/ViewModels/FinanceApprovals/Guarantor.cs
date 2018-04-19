using System.Collections.Generic;

namespace Onboarding.ViewModels.FinanceApprovals
{
    public class Guarantor
    {
        public string Name { get; set; }
        public string AssumedName { get; set; }
        public string Cpf { get; set; }
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

        public string Relationship { get; set; }
        public ICollection<dynamic> Documents { get; set; }
    }
}
