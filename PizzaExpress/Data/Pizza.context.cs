using Microsoft.EntityFrameworkCore;
using PizzaExpress.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PizzaExpress.Data
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizze { get; set; }
        public string DbPath { get; }
        public PizzaContext()
        {
            // Percorso completo del file database (Data/pizze.db)
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            DbPath = Path.Combine(folder, "pizze.db");

            // Crea automaticamente il database se non esiste
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Usa SQLite come database locale
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dati iniziali (solo alla prima creazione)
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Nome = "Margherita", Prezzo = 5.50m, Categoria = "Classica" },
                new Pizza { Id = 2, Nome = "Diavola", Prezzo = 6.00m, Categoria = "Speciale" },
                new Pizza { Id = 3, Nome = "Tartufo e Speck", Prezzo = 9.50m, Categoria = "Gourmet" }
            );
        }
    }
}