using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public abstract class DocumentType : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class PersonalDocument : DocumentType
    {
    }

    public class GuarantorDocument : DocumentType
    {
    }

    public class ResponsibleDocument : DocumentType
    {
    }
}
