namespace Onboarding.Models
{
    public class Plan : BaseModel
    {
        public string Name { get; set; }

        public int Installments { get; set; }

        public string DueDate { get; set; }

        public string Value { get; set; }

        public string InstallmentValue { get; set; }

        public int Guarantors { get; set; }
    }
}
