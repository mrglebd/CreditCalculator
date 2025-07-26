using CreditCalculator.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Установка культуры на ru-RU
var defaultCulture = new CultureInfo("ru-RU");
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// Services
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new CustomBinderProvider());
});
builder.Services.AddScoped<ICreditCalculator, CreditCalculator.Services.CreditCalculator>();
builder.Services.AddScoped<IRepaymentScheduleCalculator, AnnualScheduleCalculator>();
builder.Services.AddScoped<IRepaymentScheduleCalculator, DailyScheduleCalculator>();


var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

//Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Credit}/{action=Index}/{id?}");

app.Run();
