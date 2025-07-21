using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class InvariantDecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
            return Task.CompletedTask;
        
        decimal result;
        var normalized = value.Replace(" ", "").Replace(".", ",");

        if (decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.CurrentCulture, out result) ||
            decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"Некорректное значение: {value}");
        }

        return Task.CompletedTask;
    }
}