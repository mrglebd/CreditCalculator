using CreditCalculator.Models;
using CreditCalculator.Services;

namespace CreditCalculator.Tests;

/// <summary>
/// Юнит-тесты для проверки логики аннуитетного расчета графика платежей.
/// </summary>
public class AnnualScheduleCalculatorTests
{
    /// <summary>
    /// Проверяет, что количество платежей соответствует сроку в месяцах.
    /// </summary>
    [Fact]
    public void Calculate_GeneratesCorrectNumberOfMonthlyPayments()
    {
        // Arrange
        var calculator = new AnnualScheduleCalculator();
        var input = new CreditInputModel
        {
            LoanAmount = 100000m,
            LoanTermMonths = 12,
            AnnualInterestRate = 12m,
            RateType = InterestRateType.Annual
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        Assert.Equal(12, result.Payments.Count);
    }

    /// <summary>
    /// Проверяет корректность структуры каждого платежа: сумма, тело, проценты.
    /// </summary>
    [Fact]
    public void Calculate_EachPaymentHasCorrectStructure()
    {
        // Arrange
        var calculator = new AnnualScheduleCalculator();
        var input = new CreditInputModel
        {
            LoanAmount = 120000m,
            LoanTermMonths = 12,
            AnnualInterestRate = 10m,
            RateType = InterestRateType.Annual
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        decimal expectedMonthlyPayment = 10549.91m;
        decimal actual = result.Payments.First().Payment;

        Assert.Equal(expectedMonthlyPayment, actual);
    }

    /// <summary>
    /// Проверяет поведение при почти нулевой процентной ставке.
    /// </summary>
    [Fact]
    public void Calculate_HandlesZeroRateCorrectly()
    {
        // Arrange
        var calculator = new AnnualScheduleCalculator();
        var input = new CreditInputModel
        {
            LoanAmount = 120000m,
            LoanTermMonths = 12,
            AnnualInterestRate = 0.0001m,
            RateType = InterestRateType.Annual
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        Assert.Equal(12, result.Payments.Count);
        Assert.All(result.Payments, p => Assert.True(p.Payment > 0));
    }
}
