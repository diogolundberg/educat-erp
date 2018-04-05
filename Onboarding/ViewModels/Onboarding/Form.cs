using System.Collections.Generic;

namespace Onboarding.ViewModels.Onboarding
{
    public class Form
    {
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
