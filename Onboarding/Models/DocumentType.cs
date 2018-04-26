using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace onboarding.Models
{
    public abstract class DocumentType : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public string Validations { get; set; }

        [NotMapped]
        [JsonProperty("Validations")]
        public List<string> ValidationList
        {
            get
            {
                return Validations != null ? JsonConvert.DeserializeObject<List<string>>(Validations) : new List<string>();
            }
        }
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
