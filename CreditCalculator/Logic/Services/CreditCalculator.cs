using CreditCalculator.Models;

namespace CreditCalculator.Services
{
    public class CreditCalculator : ICreditCalculator
    {
        /// <summary>
        /// Выполняет расчет графика платежей на основе выбранного типа процентной ставки.
        /// </summary>
        /// <param name="input">Входные данные займа</param>
        /// <returns>Модель с результатами расчёта и списком платежей</returns>
        public ResultViewModel CalculateSchedule(CreditInputModel input)
        {
            return input.RateType switch
            {
                InterestRateType.Annual => CalculateAnnualSchedule(input),
                InterestRateType.Daily => CalculateDailySchedule(input),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Выполняет расчет графика аннуитетных платежей (годовая ставка).
        /// </summary>
        /// <param name="input">Входные данные займа</param>
        /// <returns>Модель с результатами расчёта и списком ежемесячных платежей</returns>
        private ResultViewModel CalculateAnnualSchedule(CreditInputModel input)
        {
            decimal loanAmount = input.LoanAmount;
            int months = input.LoanTermMonths!.Value;
            double annualRate = input.AnnualInterestRate!.Value;

            double monthlyRate = annualRate / 12 / 100;
            decimal monthlyPayment = CalculateAnnuityPayment(loanAmount, months, monthlyRate);

            var payments = GenerateMonthlySchedule(loanAmount, months, monthlyRate, monthlyPayment);

            return new ResultViewModel
            {
                Payments = payments,
                Overpayment = Math.Round(CalculateTotalInterest(payments), 2)
            };
        }

        /// <summary>
        /// Выполняет расчет графика платежей по дневной ставке с шагом.
        /// </summary>
        /// <param name="input">Входные данные займа</param>
        /// <returns>Модель с результатами расчёта и списком платежей по дням</returns>
        private ResultViewModel CalculateDailySchedule(CreditInputModel input)
        {
            decimal loanAmount = input.LoanAmount;
            int totalDays = input.LoanTermDays!.Value;
            int stepDays = input.PaymentStepDays!.Value;
            double dailyRate = input.DailyInterestRate!.Value;

            if (stepDays <= 0 || totalDays <= 0)
                throw new ArgumentException("Invalid loan duration or step");

            var payments = GenerateDailySchedule(loanAmount, totalDays, stepDays, dailyRate);

            return new ResultViewModel
            {
                Payments = payments,
                Overpayment = Math.Round(CalculateTotalInterest(payments), 2)
            };
        }

        /// <summary>
        /// Рассчитывает ежемесячный аннуитетный платеж.
        /// </summary>
        /// <param name="loanAmount">Сумма займа</param>
        /// <param name="months">Срок займа в месяцах</param>
        /// <param name="monthlyRate">Месячная процентная ставка</param>
        /// <returns>Размер ежемесячного аннуитетного платежа</returns>
        private decimal CalculateAnnuityPayment(decimal loanAmount, int months, double monthlyRate)
        {
            return loanAmount *
                   (decimal)(monthlyRate * Math.Pow(1 + monthlyRate, months) /
                             (Math.Pow(1 + monthlyRate, months) - 1));
        }

        /// <summary>
        /// Генерирует график ежемесячных аннуитетных платежей.
        /// </summary>
        /// <param name="loanAmount">Сумма займа</param>
        /// <param name="months">Срок займа в месяцах</param>
        /// <param name="monthlyRate">Месячная процентная ставка</param>
        /// <param name="monthlyPayment">Рассчитанный ежемесячный платеж</param>
        /// <returns>Список платежей по месяцам</returns>
        private List<PaymentInfo> GenerateMonthlySchedule(decimal loanAmount, int months, double monthlyRate, decimal monthlyPayment)
        {
            var payments = new List<PaymentInfo>();
            decimal remainingDebt = loanAmount;

            for (int i = 1; i <= months; i++)
            {
                decimal interest = remainingDebt * (decimal)monthlyRate;
                decimal principal = monthlyPayment - interest;
                remainingDebt -= principal;
                if (remainingDebt < 0) remainingDebt = 0;

                payments.Add(new PaymentInfo
                {
                    Number = i,
                    Date = DateTime.Now.AddMonths(i),
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    RemainingDebt = Math.Round(remainingDebt, 2)
                });
            }

            return payments;
        }

        /// <summary>
        /// Генерирует график платежей с дневной ставкой и заданным шагом.
        /// </summary>
        /// <param name="loanAmount">Сумма займа</param>
        /// <param name="totalDays">Общий срок займа в днях</param>
        /// <param name="stepDays">Шаг между платежами в днях</param>
        /// <param name="dailyRate">Дневная процентная ставка</param>
        /// <returns>Список платежей по дням</returns>
        private List<PaymentInfo> GenerateDailySchedule(decimal loanAmount, int totalDays, int stepDays, double dailyRate)
        {
            var payments = new List<PaymentInfo>();
            int numberOfPayments = totalDays / stepDays;
            decimal remainingDebt = loanAmount;
            DateTime startDate = DateTime.Now;

            for (int i = 1; i <= numberOfPayments; i++)
            {
                decimal principal = loanAmount / numberOfPayments;
                decimal interest = remainingDebt * (decimal)(dailyRate / 100) * stepDays;
                remainingDebt -= principal;
                if (remainingDebt < 0) remainingDebt = 0;

                payments.Add(new PaymentInfo
                {
                    Number = i,
                    Date = startDate.AddDays(i * stepDays),
                    Principal = Math.Round(principal, 2),
                    Interest = Math.Round(interest, 2),
                    RemainingDebt = Math.Round(remainingDebt, 2)
                });
            }

            return payments;
        }

        /// <summary>
        /// Вычисляет общую сумму процентов по всем платежам.
        /// </summary>
        /// <param name="payments">Список платежей</param>
        /// <returns>Общая сумма переплаты</returns>
        private decimal CalculateTotalInterest(List<PaymentInfo> payments)
        {
            decimal total = 0;
            foreach (var payment in payments)
            {
                total += payment.Interest;
            }
            return total;
        }
    }
}
