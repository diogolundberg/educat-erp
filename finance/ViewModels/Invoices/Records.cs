using System;

namespace finance.ViewModels.invoices
{
    public class Records
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
    }
}
