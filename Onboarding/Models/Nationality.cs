namespace onboarding.Models
{
    public class Nationality : BaseModel
    {
        public string Name { get; set; }

        public bool CheckForeign { get; set; }

        public bool CheckNative { get; set; }
    }
}
