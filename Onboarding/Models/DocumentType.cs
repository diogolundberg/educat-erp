using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public abstract class DocumentType : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class PersonalDocumentType : DocumentType
    {
    }

    public class GuarantorDocumentType : DocumentType
    {
    }

    public class ResponsibleDocumentType : DocumentType
    {
    }
}
