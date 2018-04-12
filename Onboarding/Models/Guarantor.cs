using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class Guarantor : BaseModel
    {
        public Guarantor()
        {
            GuarantorDocuments = new HashSet<GuarantorDocument>();
        }

        public string Cpf { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string AddressNumber { get; set; }

        public string ComplementAddress { get; set; }

        public string Neighborhood { get; set; }

        public string PhoneNumber { get; set; }

        public string Landline { get; set; }

        public string Email { get; set; }

        public string Zipcode { get; set; }

        [ForeignKey("AddressKind")]
        public int? AddressKindId { get; set; }

        [JsonIgnore]
        public virtual AddressKind AddressKind { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        [JsonIgnore]
        public virtual City City { get; set; }

        [ForeignKey("State")]
        public int? StateId { get; set; }

        [JsonIgnore]
        public virtual State State { get; set; }

        [ForeignKey("FinanceData")]
        public int? FinanceDataId { get; set; }

        [JsonIgnore]
        public virtual FinanceData FinanceData { get; set; }

        [ForeignKey("Relationship")]
        public int? RelationshipId { get; set; }

        public virtual Relationship Relationship { get; set; }

        public virtual ICollection<GuarantorDocument> GuarantorDocuments { get; set; }
    }
}
