using System.Collections.Generic;

namespace Onboarding.ViewModels.Onboarding
{
    public class Form
    {
        public List<Item> Items { get; set; }
        public string Deadline { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
