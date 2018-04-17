using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Onboarding.Models
{
    public abstract class Representative : BaseModel
    {
        [ForeignKey("FinanceData")]
        public int? FinanceDataId { get; set; }

        [JsonIgnore]
        public virtual FinanceData FinanceData { get; set; }

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
    }

    public class RepresentativePerson : Representative
    {
        [NotMapped]
        private string _Cpf { get; set; }
        public string Cpf
        {
            get
            {
                return _Cpf;
            }
            set
            {
                if (value != null && value.Count() == 11)
                {
                    _Cpf = Convert.ToUInt64(value).ToString(@"000\.000\.000\-00");
                }
                else
                {
                    _Cpf = value;
                }
            }
        }

        [ForeignKey("Relationship")]
        public int? RelationshipId { get; set; }

        public virtual Relationship Relationship { get; set; }
    }

    public class RepresentativeCompany : Representative
    {
        public string Cnpj { get; set; }

        public string Contact { get; set; }
    }
}
