using Microsoft.EntityFrameworkCore;
using PizzaExpress.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); // Abilita anche XML (Accept: text/xml)

// DB InMemory per semplicità didattica
builder.Services.AddDbContext<PizzaContext>(opt => opt.UseInMemoryDatabase("dbpizze"));

SQLITE_DB.inizialize("pizze.db");

// Swagger (interfaccia di test)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed dati demo
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PizzaContext>();
    SeedData.Initialize(ctx);
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
