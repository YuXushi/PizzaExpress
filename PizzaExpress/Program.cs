using PizzaExpress.Data;
using PizzaExpress.Repository;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

// Ottiene IP dell'host nella LAN
string ipRete = "NON DISPONIBILE";

try
{
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList)
    {
        if (ip.AddressFamily == AddressFamily.InterNetwork &&
            !IPAddress.IsLoopback(ip))
        {
            ipRete = ip.ToString();
            break;
        }
    }
}
catch
{
    ipRete = "ERRORE";
}

var builder = WebApplication.CreateBuilder(args);


// Aggiungi servizi
builder.Services.AddDbContext<PizzaContext>();
builder.Services.AddScoped<PizzaRepository>();
builder.Services.AddControllers();

var app = builder.Build();

// Risponde all'endpoint "/" con i dati del server e dell'API
app.MapGet("/", () =>
{
    return Results.Text(
        "PizzaExpress Server Online\n\n" +
        $"Server in ascolto su:\n" +
        $"http://localhost:5000\n" +
        $"http://127.0.0.1:5000\n" +
        $"http://{ipRete}:5000\n\n" +
        "API:\n" +
        $"http://{ipRete}:5000/api/pizze\n",
        "text/plain"
    );
});

// Mappa l'API
app.MapControllers();

app.Urls.Add("http://0.0.0.0:5000");

Console.WriteLine("=================================");
Console.WriteLine(" PIZZGRA SERVER ONLINE");
Console.WriteLine(" In ascolto su:");
Console.WriteLine(" http://localhost:5000");
Console.WriteLine($" http://{ipRete}:5000");
Console.WriteLine("=================================");

// Apre il browser automaticamente
app.Lifetime.ApplicationStarted.Register(() =>
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "http://localhost:5000",
            UseShellExecute = true
        });
    }
    catch { }
});

app.Run();