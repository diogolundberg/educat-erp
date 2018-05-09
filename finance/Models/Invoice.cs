using System;
using System.Collections.Generic;

namespace finance.Models
{
    public class Invoice : BaseModel
    {
        public Invoice()
        {
            InvoiceItens = new HashSet<InvoiceItem>();
        }

        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItens { get; set; }
    }
}
