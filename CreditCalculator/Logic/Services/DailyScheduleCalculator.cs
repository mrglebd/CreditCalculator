using CreditCalculator.Models;
using CreditCalculator.Services;

public class DailyScheduleCalculator : IRepaymentScheduleCalculator
{
    public InterestRateType RateType => InterestRateType.Daily;

    /// <summary>
    /// Выполняет расчет графика платежей на основе дневной процентной ставки и указанного шага.
    /// </summary>
    /// <param name="input">Входная модель с параметрами займа</param>
    /// <returns>Результат с расчетным графиком и общей переплатой</returns>
    /// <exception cref="ArgumentException">Бросается при некорректных значениях срока или шага платежа</exception>
    public ResultViewModel Calculate(CreditInputModel input)
    {
        decimal loanAmount = Math.Round(input.LoanAmount, 2);
        int totalDays = input.LoanTermDays!.Value;
        int stepDays = input.PaymentStepDays!.Value;
        double dailyRate = input.DailyInterestRate!.Value;

        if (stepDays <= 0 || totalDays <= 0)
            throw new ArgumentException("Invalid loan duration or step");

        int numberOfPayments = (int)Math.Ceiling((double)totalDays / stepDays);
        double periodRate = (dailyRate / 100) * stepDays;
        
        decimal payment = loanAmount *
                          (decimal)(periodRate * Math.Pow(1 + periodRate, numberOfPayments) /
                                    (Math.Pow(1 + periodRate, numberOfPayments) - 1));

        var payments = new List<PaymentInfo>();
        decimal remainingDebt = loanAmount;
        DateTime startDate = DateTime.Now;

        for (int i = 1; i <= numberOfPayments; i++)
        {
            decimal interest = remainingDebt * (decimal)periodRate;
            decimal principal = payment - interest;
            remainingDebt -= principal;

            if (remainingDebt < 0) remainingDebt = 0;

            payments.Add(new PaymentInfo
            {
                Number = i,
                Date = startDate.AddDays(i * stepDays),
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