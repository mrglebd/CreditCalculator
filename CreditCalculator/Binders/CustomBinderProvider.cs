using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CustomBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
        {
            return new InvariantDecimalModelBinder();
        }

        if (context.Metadata.ModelType == typeof(double) || context.Metadata.ModelType == typeof(double?))
        {
            return new InvariantDoubleModelBinder();
        }

        return null!;
    }
}