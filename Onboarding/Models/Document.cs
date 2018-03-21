using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class Document : BaseModel
    {
        public string Link { get; set; }

        public Guid DocumentTypeId { get; set; }

        [ForeignKey("DocumentTypeId")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
