using Microsoft.AspNetCore.Mvc;
using PizzaExpress.Models;

namespace PizzaExpress.Controllers
{
    public class Ingredienti
    {
        [ApiController]
        [Route("api/[controller]")]
        public class IngredientiController : ControllerBase
        {
            private readonly SpoonacularAPI _spoonacularAPI;
            public IngredientiController(SpoonacularAPI spoonacularAPI)
            {
                _spoonacularAPI = spoonacularAPI;
            }

            [HttpGet("da-pizza")]
            public async Task<IActionResult> GetIngredientiDaPizza([FromQuery] string nome)
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    return BadRequest("Nome pizza mancante");
                }
                
                var ingredienti = await _spoonacularAPI.GetIngredientiDaPizzaAsync(nome);

                return Ok(new
                {
                    pizza = nome,
                    ingredienti = ingredienti,
                    fonte = "Spoonacular"
                });
            }
        }
    }
}
