using System.ComponentModel.DataAnnotations;

namespace CreditCalculator.Models
{
    public enum InterestRateType
    {
        Annual,
        Daily
    }

    public class CreditInputModel : IValidatableObject
    {
        [Required(ErrorMessage = "Выберите тип процентной ставки")]
        [Display(Name = "Тип процентной ставки")]
        public InterestRateType? RateType { get; set; }

        [Required(ErrorMessage = "Сумма займа обязательна")]
        [Range(1, 100_000_000, ErrorMessage = "Сумма должна быть от 1 до 100 000 000")]
        [Display(Name = "Сумма займа")]
        public decimal LoanAmount { get; set; }

        [Display(Name = "Срок займа (в месяцах)")]
        public int? LoanTermMonths { get; set; }

        [Display(Name = "Годовая процентная ставка")]
        public decimal? AnnualInterestRate { get; set; }

        [Display(Name = "Срок займа (в днях)")]
        public int? LoanTermDays { get; set; }

        [Display(Name = "Ставка в день")] 
        public decimal? DailyInterestRate { get; set; }

        [Display(Name = "Шаг платежа (в днях)")]
        public int? PaymentStepDays { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RateType == InterestRateType.Annual)
            {
                if (!LoanTermMonths.HasValue || LoanTermMonths <= 0)
                    yield return new ValidationResult("Введите срок займа в месяцах", new[] { nameof(LoanTermMonths) });

                if (!AnnualInterestRate.HasValue || AnnualInterestRate <= 0 || AnnualInterestRate > 100)
                    yield return new ValidationResult("Введите годовую ставку от 0.1 до 100",
                        new[] { nameof(AnnualInterestRate) });
            }
            else if (RateType == InterestRateType.Daily)
            {
                if (!LoanTermDays.HasValue || LoanTermDays <= 0)
                    yield return new ValidationResult("Введите срок займа в днях", new[] { nameof(LoanTermDays) });

                if (!DailyInterestRate.HasValue || DailyInterestRate <= 0 || DailyInterestRate > 100)
                    yield return new ValidationResult("Введите дневную ставку от 0.1 до 100",
                        new[] { nameof(DailyInterestRate) });

                if (!PaymentStepDays.HasValue || PaymentStepDays <= 0 || PaymentStepDays > 365)
                    yield return new ValidationResult("Введите шаг платежа от 1 до 365 дней",
                        new[] { nameof(PaymentStepDays) });
            }
        }
    }
}