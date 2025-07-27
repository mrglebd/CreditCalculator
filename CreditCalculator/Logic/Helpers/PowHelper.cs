namespace CreditCalculator.Helpers
{
    /// <summary>
    /// Вспомогательные методы для финансовых расчетов.
    /// </summary>
    public static class FinanceHelper
    {
        /// <summary>
        /// Возводит число decimal в положительную целую степень.
        /// </summary>
        /// <param name="baseValue">Основание степени</param>
        /// <param name="exponent">Показатель степени (целое число)</param>
        /// <returns>baseValue, возведенное в степень exponent</returns>
        public static decimal Pow(decimal baseValue, int exponent)
        {
            decimal result = 1m;
            for (int i = 0; i < exponent; i++)
                result *= baseValue;
            return result;
        }
    }
}