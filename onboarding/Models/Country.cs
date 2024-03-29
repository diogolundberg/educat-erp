using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public class Country : BaseModel
    {
        public Country ()
        {
        }

        [Required]
        public string Name { get; set; }

        public bool CheckForeignGraduation { get; set; }

        public bool HasUF { get; set; }
    }
}
