using Microsoft.AspNetCore.Mvc;
using CreditCalculator.Models;
using CreditCalculator.Services;

namespace CreditCalculator.Controllers
{
    /// <summary>
    /// Контроллер для обработки ввода данных по кредиту и отображения результатов расчета.
    /// </summary>
    public class CreditController : Controller
    {
        private readonly ICreditCalculator _creditCalculator;
        
        public CreditController(ICreditCalculator creditCalculator)
        {
            _creditCalculator = creditCalculator;
        }

        /// <summary>
        /// Отображает стартовую форму ввода данных по кредиту.
        /// </summary>
        /// <returns>Представление Index с пустой моделью</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CreditInputModel());
        }

        /// <summary>
        /// Обрабатывает отправку формы, выполняет валидацию и расчет графика платежей.
        /// </summary>
        /// <param name="model">Модель данных кредита, введённая пользователем</param>
        /// <returns>
        /// Представление Result с расчетами, либо возвращает на форму Index при ошибках валидации.
        /// </returns>
        [HttpPost]
        public IActionResult Calculate(CreditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var result = _creditCalculator.CalculateSchedule(model);
            return View("Result", result);
        }
    }
}