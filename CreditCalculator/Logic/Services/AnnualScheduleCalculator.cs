using CreditCalculator.Helpers;
using CreditCalculator.Models;

namespace CreditCalculator.Services
{
    public class AnnualScheduleCalculator : IRepaymentScheduleCalculator
    {
        public InterestRateType RateType => InterestRateType.Annual;

        /// <summary>
        /// Выполняет расчет графика аннуитетных ежемесячных платежей на основе годовой ставки.
        /// </summary>
        /// <param name="input">Входная модель с параметрами кредита</param>
        /// <returns>Результирующая модель с графиком платежей и суммой переплаты</returns>
        public ResultViewModel Calculate(CreditInputModel input)
        {
            decimal loanAmount = Math.Round(input.LoanAmount, 2);
            int months = input.LoanTermMonths!.Value;
            decimal annualRate = input.AnnualInterestRate!.Value;
            decimal monthlyRate = annualRate / 12m / 100m;

            decimal pow = FinanceHelper.Pow(1 + monthlyRate, months);
            decimal monthlyPayment = loanAmount * monthlyRate * pow / (pow - 1);

            List<PaymentInfo> payments = new List<PaymentInfo>();
            decimal remainingDebt = loanAmount;

            for (int i = 1; i <= months; i++)
            {
                decimal interest = remainingDebt * monthlyRate;
                decimal principal = monthlyPayment - interest;
                remainingDebt -= principal;

                if (remainingDebt < 0) remainingDebt = 0;

                payments.Add(new PaymentInfo
                {
                    Number = i,
                    Date = DateTime.Now.AddMonths(i),
                    Payment = Math.Round(principal + interest, 2),
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    RemainingDebt = Math.Round(remainingDebt, 2)
                });
            }

            return new ResultViewModel
            {
                Payments = payments,
                Overpayment = Math.Round(payments.Sum(p => p.Interest), 2)
            };
        }
    }
}