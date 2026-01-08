using Microsoft.EntityFrameworkCore;
using PizzaExpress.Data;
using PizzaExpress.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); // Abilita anche XML (Accept: text/xml)

builder.Services.AddScoped<PizzaRepository>();


// DB InMemory per semplicità didattica
builder.Services.AddDbContext<PizzaContext>();


// Swagger (interfaccia di test)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed dati demo
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PizzaContext>();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
