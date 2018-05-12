using System.Collections.Generic;

namespace finance.ViewModels.Invoices
{
    public class Form
    {
        public Form()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string DueDate { get; set; }
        public string Billet { get; set; }
        public ICollection<Item> Items { get; set; }
        public Representative Representative { get; set; }
    }
}
