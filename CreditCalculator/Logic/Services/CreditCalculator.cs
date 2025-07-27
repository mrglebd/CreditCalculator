using CreditCalculator.Models;

namespace CreditCalculator.Services
{
    public class CreditCalculator : ICreditCalculator
    {
        private readonly AnnualScheduleCalculator _annualCalculator;
        private readonly DailyScheduleCalculator _dailyCalculator;

        public CreditCalculator(AnnualScheduleCalculator annualCalculator, DailyScheduleCalculator dailyCalculator)
        {
            _annualCalculator = annualCalculator;
            _dailyCalculator = dailyCalculator;
        }

        /// <summary>
        /// Выполняет расчет графика платежей, выбирая нужную стратегию на основе типа процентной ставки.
        /// </summary>
        /// <param name="input">Входная модель с параметрами кредита</param>
        /// <returns>Результирующая модель с графиком платежей и суммой переплаты</returns>
        /// <exception cref="InvalidOperationException">Выбрана неподдерживаемая стратегия расчета</exception>
        public ResultViewModel CalculateSchedule(CreditInputModel input)
        {
            IRepaymentScheduleCalculator strategy = input.RateType switch
            {
                InterestRateType.Annual => _annualCalculator,
                InterestRateType.Daily => _dailyCalculator,
                _ => throw new InvalidOperationException($"Неподдерживаемый тип ставки: {input.RateType}")
            };

            return strategy.Calculate(input);
        }
    }
}