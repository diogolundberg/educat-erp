using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class BaseModel : ICloneable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual string CreateExternalId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
