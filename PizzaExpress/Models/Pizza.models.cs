using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaExpress.Models
{
    public class Pizza
    {
        // Modello della pizza
        // Quindi tutto le pizze devono avere questo modello
        // Ossia, devono tutti avere un Id, un Nome, un Prezzo e una Categoria
        // public int Id { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Prezzo { get; set; }
        public string Categoria {  get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int Tavolo { get; set; }
        public string Stato { get; set; } = string.Empty;

        // Override della funzione ToString default
        public override string ToString()
        {
            return $"{Id} - {Nome} ({Categoria}) - €{Prezzo:F2} | Tavolo {Tavolo} - Stato: {Stato}";
        }
    }
}
