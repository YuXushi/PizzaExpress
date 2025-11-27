namespace PizzaExpress.Models
{
    public class Pizza
    {
        // Modello della pizza
        // Quindi tutto le pizze devono avere questo modello
        // Ossia, devono tutti avere un Id, un Nome, un Prezzo e una Categoria
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Prezzo { get; set; }
        public string Categoria {  get; set; } = string.Empty;
    }
}
