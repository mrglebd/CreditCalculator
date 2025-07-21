using CreditCalculator.Models;

namespace CreditCalculator.Services
{
    public interface ICreditCalculator
    {
        /// <summary>
        /// Выполняет расчет графика платежей на основе типа процентной ставки.
        /// </summary>
        /// <param name="input">Входная модель займа</param>
        /// <returns>Результат с платежами и переплатой</returns>
        ResultViewModel CalculateSchedule(CreditInputModel input);
    }
}