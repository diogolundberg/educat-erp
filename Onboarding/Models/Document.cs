using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class Document : BaseModel
    {
        public string Url { get; set; }

        [ForeignKey("DocumentType")]
        public int DocumentTypeId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}
