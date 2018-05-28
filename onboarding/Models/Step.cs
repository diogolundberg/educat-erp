namespace onboarding.Models
{
    public class Step : BaseModel
    {
        public string Name { get; set; }
        public string Resource { get; set; }
        public int Order { get; set; }
    }
}
