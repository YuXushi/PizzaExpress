using Microsoft.EntityFrameworkCore;
using PizzaExpress.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PizzaExpress.Data
{
    // DB context in questo caso utilizza il EntityFrameworkCore
    // In un vero API, qui ci sarebbe le funzioni per aggiungere e prendere i dati dal database
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }
        public DbSet<Pizza> Pizze => Set<Pizza>();
    }
    public static class SeedData
    {
        // Inizializza la demo dei dati
        public static void Initialize(PizzaContext ctx)
        {
            if (ctx.Pizze.Any()) return;
            // Quindi aggiunge questi nuovi oggetti al context (ossia la memoria)
            ctx.Pizze.AddRange(
                new Pizza { Id = 1, Nome = "Margherita", Prezzo = 4.50m, Categoria = "Classico" },
                new Pizza { Id = 2, Nome = "Prosciutto", Prezzo = 5.50m, Categoria = "Classico" },
                new Pizza { Id = 3, Nome = "Capricciosa", Prezzo = 7.00m, Categoria = "Speciale" }
            );
            ctx.SaveChanges();
        }
    }
    public class PizzaContex : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }
        public string dbPath { get; }
        public PizzaContex()
        {
            // Trova i folder
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "DB");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            dbPath = Path.Combine(folder, "pizze.db");

            // In qualche modo crea il database se non esiste
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Usa SQLite come database locale
            options.UseSqlite($"Data Source={dbPath}");
            // Utilizzare versione del pacchetto Microsoft.EntityFrameworkCore.Sqlite 9.0.11
        }
    }
}