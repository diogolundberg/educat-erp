using System;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Plan : BaseModel
    {
        public string Name { get; set; }

        public int Installments { get; set; }

        public DateTime? DueDate { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public string InstallmentValue { get; set; }

        public int Guarantors { get; set; }
    }
}
