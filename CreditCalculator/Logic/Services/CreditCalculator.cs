using CreditCalculator.Models;

namespace CreditCalculator.Services
{
    public class CreditCalculator : ICreditCalculator
    {
        private readonly IEnumerable<IRepaymentScheduleCalculator> _strategies;

        public CreditCalculator(IEnumerable<IRepaymentScheduleCalculator> strategies)
        {
            _strategies = strategies;
        }

        /// <summary>
        /// Выполняет расчет графика платежей, выбирая нужную стратегию на основе типа процентной ставки.
        /// </summary>
        /// <param name="input">Входная модель с параметрами кредита</param>
        /// <returns>Результирующая модель с графиком платежей и суммой переплаты</returns>
        /// <exception cref="InvalidOperationException">Выбрана неподдерживаемая стратегия расчета</exception>
        public ResultViewModel CalculateSchedule(CreditInputModel input)
        {
            var strategy = _strategies.FirstOrDefault(s => s.RateType == input.RateType);
            if (strategy == null)
                throw new InvalidOperationException("Не найдена подходящая стратегия расчета");

            return strategy.Calculate(input);
        }
    }
}