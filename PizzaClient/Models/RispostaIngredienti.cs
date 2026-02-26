using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaClient.Models
{
    public class RispostaIngredienti
    {
        public string pizza { get; set; }
        public List<string> ingredienti { get; set; }
        public string fonte { get; set; }
    }
}
