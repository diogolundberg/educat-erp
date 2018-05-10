namespace billet.ViewModels
{
    public class Billet
    {
        public short BankCode { get; set; }
        public string WalletNumber { get; set; }
        public string DueDate { get; set; }
        public decimal Value { get; set; }
        public string DocumentNumber { get; set; }
        public Assignor Assignor { get; set; }
        public Payer Payer { get; set; }
    }

    public class Assignor
    {
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string Agency { get; set; }
        public string AccountBank { get; set; }
    }

    public class Payer
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Cep { get; set; }
        public string State { get; set; }
    }
}