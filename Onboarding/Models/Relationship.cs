namespace Onboarding.Models
{
    public class Relationship : BaseModel
    {
        public string Name { get; set; }

        public bool CheckSpouse { get; set; }

        public bool CheckStudentIsRepresentative { get; set; }
    }
}
