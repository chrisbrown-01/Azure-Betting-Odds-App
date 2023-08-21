using BettingOddsCalculator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace BettingOddsCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //string connectionString = builder.Configuration.GetConnectionString("AppConfig") ?? throw new InvalidOperationException("Connection string not found");
            string azureFunctionUrl = builder.Configuration.GetValue<string>("AzureFunctionUrl") ?? throw new InvalidOperationException("Azure function URL string not found");
            string azureFunctionKey = builder.Configuration.GetValue<string>("AzureFunctionKey") ?? throw new InvalidOperationException("Azure function header key string not found");

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<ICalculations, Calculations>();

            builder.Services.AddHttpClient("SearchMmaFighter", httpClient =>
            {
                httpClient.BaseAddress = new Uri(azureFunctionUrl);
                httpClient.DefaultRequestHeaders.Add("x-functions-key", azureFunctionKey);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}