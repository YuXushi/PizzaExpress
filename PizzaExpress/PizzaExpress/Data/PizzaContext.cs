using Microsoft.EntityFrameworkCore;
using PizzaExpress.Models;
using Microsoft.Data.Sqlite;

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
    public class SQLITE_DB
    {
        static public void inizialize(string db_path)
        {
            using (var connection = new SqliteConnection($"Data Source={db_path}"))
            {
                connection.Open();
                try
                {
                    var command = connection.CreateCommand();

                    command.CommandText =
                    @"
                        CREATE TABLE IF NOT EXISTS pizze (
                            Id INTEGER PRIMARY KEY,
                            Nome TEXT NOT NULL,
                            Prezzo REAL NOT NULL,
                            Categoria TEXT NOT NULL,
                            Note TEXT
                        );
                    ";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore nella creazione del database" + ex.ToString());
                }
                finally
                {
                    connection.Close();
                }

                /*
                command.CommandText =
                @"
                    SELECT name
                    FROM user
                    WHERE id = $id
                ";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
                */
            }
        }

        // Get
        // Get + Query
        // Get + Post Query
        static public List<Pizza> getPizze(string db_path)
        {
            using (var connection = new SqliteConnection($"Data Source={db_path}"))
            {
                connection.Open();
                try
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore nella creazione del database" + ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            return [];
        }

        // Post

        // Put

        // Delete
    }
}