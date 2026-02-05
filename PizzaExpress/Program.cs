using PizzaExpress.Data;
using PizzaExpress.Repository;

Console.WriteLine("Avviando server web api...");
var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<PizzaContext>();
builder.Services.AddScoped<PizzaRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Urls.Add("http://localhost:5000");

app.Run();