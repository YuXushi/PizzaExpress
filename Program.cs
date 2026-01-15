using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PIZZGRA.Data;
using PIZZGRA.Api;
using System;
using System.Windows.Forms;

namespace PIZZGRA
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //----------------------------------------------------------
            // 1) AVVIO API REST IN BACKGROUND (NUOVO MODELLO .NET 8)
            //----------------------------------------------------------
            Task.Run(() =>
            {
                var builder = WebApplication.CreateBuilder();

                builder.Services.AddDbContext<PizzaDbContext>();
                builder.Services.AddScoped<PizzaRepository>();
                builder.Services.AddControllers();

                var app = builder.Build();

                app.MapControllers();

                app.Urls.Add("http://localhost:5000");

                app.Run();
            });

            //----------------------------------------------------------
            // 2) AVVIO GUI WINFORMS
            //----------------------------------------------------------
            ApplicationConfiguration.Initialize();
            Application.Run(new GestionePizzeForm());
        }
    }
}
