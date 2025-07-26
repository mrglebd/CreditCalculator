using CreditCalculator.Models;

namespace CreditCalculator.Services
{
    public interface IRepaymentScheduleCalculator
    {
        /// <summary>
        /// Выполняет расчет графика платежей на основе входных параметров займа.
        /// </summary>
        /// <param name="input">Входная модель с параметрами кредита</param>
        /// <returns>Результирующая модель с графиком платежей и переплатой</returns>
        ResultViewModel Calculate(CreditInputModel input);
        
        /// <summary>
        /// Тип процентной ставки, для которой применяется данный алгоритм расчета.
        /// </summary>
        InterestRateType RateType { get; }
    }
}