using System;

namespace billet.ViewModels
{
    public class Billet
    {
        public short BankCode { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public string PayerName { get; set; }
        public string PayerDocument { get; set; }
        public string PayerAddress { get; set; }
        public string PayerDistrict { get; set; }
        public string PayerCity { get; set; }
        public string PayerCep { get; set; }
        public string PayerState { get; set; }
    }
}