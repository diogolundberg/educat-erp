using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Onboarding.Models
{
    public class Enrollment : BaseModel
    {
        public Enrollment ()
        {
        }

        public virtual PersonalData PersonalData { get; set; }

        public DateTime? SendBy { get; set; }
    }
}

