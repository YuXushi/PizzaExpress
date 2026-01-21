using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIZZGRA.Models
{
    public class Pizza
    {
        // Chiave primaria generata automaticamente (IDENTITY)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nome della pizza
        public string Nome { get; set; } = string.Empty;

        // Prezzo della pizza
        public decimal Prezzo { get; set; }

        // Categoria della pizza
        public string Categoria { get; set; } = "Classica";

        // NOTE: Campo note aggiuntivo richiesto dal progetto
        public string Note { get; set; } = string.Empty;
        // public string Tavolo { get; set; } = string.Empty;
        public string Stato {  get; set; } = "Preparazione";
        public override string ToString()
        {
            // return $"{Id} - {Nome} ({Categoria}) - €{Prezzo:F2}";
            // return $"{Id} - {Nome} ({Categoria}) - €{Prezzo:F2} - Tavolo {Tavolo} - Status: {Stato}";
            return $"{Id} - {Nome} ({Categoria}) - €{Prezzo:F2} - Status: {Stato}";
        }
    }
}
