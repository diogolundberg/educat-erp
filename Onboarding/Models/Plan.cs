using System;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.Models
{
    public class Plan : BaseModel
    {
        public string Name { get; set; }

        public  int Installments { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Valor inválido")]
        public decimal Value { get; set; }

        public int Guarantors { get; set; }
    }
}
