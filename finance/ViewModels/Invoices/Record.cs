using System.Collections.Generic;

namespace finance.ViewModels.Invoices
{
    public class Record
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string DueDate { get; set; }
        public string Billet { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}