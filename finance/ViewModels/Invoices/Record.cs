using System.Collections.Generic;

namespace finance.ViewModels.Invoices
{
    public class Record
    {
        public Record()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string DueDate { get; set; }
        public string Billet { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public bool Compensated { get; set; }
    }
}