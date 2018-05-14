using Newtonsoft.Json;
using System.Collections.Generic;

namespace onboarding.ViewModels.Payments
{
    public class Invoice
    {
        public Invoice()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Value { get; set; }
        public string DueDate { get; set; }
        public string Billet { get; set; }
        public ICollection<Item> Items { get; set; }
        public RepresentativeViewModel Representative { get; set; }
    }
}
