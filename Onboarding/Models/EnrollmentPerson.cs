using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class EnrollmentPerson : BaseModel
    {
        public Guid? DocumentTypeId { get; set; }

        [ForeignKey("DocumentTypeId")]
        [JsonIgnore]
        public virtual DocumentType DocumentType { get; set; }

        public string Cpf { get; set; }

        public string Cnpj { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }
    }
}