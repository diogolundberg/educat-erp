using System.Collections.Generic;

namespace finance.Models
{
    public class Invoice : BaseModel
    {
        public Invoice()
        {
            InvoiceItens = new HashSet<InvoiceItem>();
        }

        public virtual ICollection<InvoiceItem> InvoiceItens { get; set; }
    }
}
