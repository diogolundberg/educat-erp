using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace onboarding.Models
{
    public class MaritalStatus : BaseModel
    {
        public MaritalStatus()
        {
        }

        [Required]
        public string Name { get; set; }
    } 
}