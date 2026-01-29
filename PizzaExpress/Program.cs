using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PizzaExpress.Data;
using PizzaExpress.Api;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace PizzaExpress
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Avviando server web api in background...");
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddDbContext<PizzaDbContext>();
            builder.Services.AddScoped<PizzaRepository>();
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();

            app.Urls.Add("http://localhost:5000");

            app.Run();
        }
    }
}