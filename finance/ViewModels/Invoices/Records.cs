using System;

namespace finance.ViewModels.Invoices
{
    public class Records
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string DueDate { get; set; }
    }
}
