using CreditCalculator.Models;
using CreditCalculator.Services;

namespace CreditCalculator.Tests;

/// <summary>
/// Юнит-тесты для дневного графика платежей с аннуитетной логикой.
/// </summary>
public class DailyScheduleCalculatorTests
{
    /// <summary>
    /// Проверяет, что количество платежей соответствует расчетному количеству шагов.
    /// </summary>
    [Fact]
    public void Calculate_GeneratesCorrectNumberOfPayments()
    {
        // Arrange
        var calculator = new DailyScheduleCalculator();
        var input = new CreditInputModel
        {
            LoanAmount = 10000m,
            LoanTermDays = 30,
            PaymentStepDays = 10,
            DailyInterestRate = 0.5m,
            RateType = InterestRateType.Daily
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        Assert.Equal(3, result.Payments.Count);
    }

    /// <summary>
    /// Проверяет, что каждый платеж состоит из валидных частей и сумма соответствует сумме тела и процентов.
    /// </summary>
    [Fact]
    public void Calculate_EachPaymentHasCorrectStructure()
    {
        // Arrange
        var calculator = new DailyScheduleCalculator();
        var input = new CreditInputModel
        {
            LoanAmount = 9000m,
            LoanTermDays = 15,
            PaymentStepDays = 5,
            DailyInterestRate = 0.6m,
            RateType = InterestRateType.Daily
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        foreach (var p in result.Payments)
        {
            decimal expected = Math.Round(p.Principal + p.Interest, 2);
            Assert.InRange(p.Payment, expected - 0.01m, expected + 0.01m);
            Assert.True(p.Principal > 0);
            Assert.True(p.Interest > 0);
        }

        Assert.True(result.Overpayment > 0);
    }

    /// <summary>
    /// Проверяет расчет первого платежа на основе фиксированных входных данных.
    /// </summary>
    [Fact]
    public void Calculate_KnownInput_ProducesExpectedFirstPayment()
    {
        // Arrange
        var calculator = new DailyScheduleCalculator();
        var input = new CreditInputModel
        {
            LoanAmount = 10000m,
            LoanTermDays = 20,
            PaymentStepDays = 10,
            DailyInterestRate = 0.5m,
            RateType = InterestRateType.Daily
        };

        // Act
        var result = calculator.Calculate(input);

        // Assert
        decimal expectedPayment = 5378.05m;
        decimal actual = result.Payments.First().Payment;
        Assert.Equal(expectedPayment, actual);
    }
}
