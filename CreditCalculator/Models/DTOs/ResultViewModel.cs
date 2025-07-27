namespace CreditCalculator.Models
{
    public class ResultViewModel
    {
        public List<PaymentInfo> Payments { get; set; } = new();
        public decimal Overpayment { get; set; }
    }
}
