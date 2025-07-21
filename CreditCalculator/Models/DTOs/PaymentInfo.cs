namespace CreditCalculator.Models
{
    public class PaymentInfo
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal RemainingDebt { get; set; }
    }
}