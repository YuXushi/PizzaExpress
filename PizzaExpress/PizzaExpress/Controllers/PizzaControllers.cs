using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaExpress.Data;
using PizzaExpress.Models;

namespace PizzaExpress.Controllers
{
    // Route è il endpoint prefix dell'API, quindi tutti i endpoint iniziano con /api/[controller] che in questo caso è pizze
    [ApiController]
    [Route("api/[controller]")]
    public class PizzeController : ControllerBase
    {
        private readonly PizzaContext _ctx;

        public PizzeController(PizzaContext ctx)
        {
            _ctx = ctx;
        }

        // GET: /api/pizze
        [HttpGet] // definisce il tipo di operazione REST
        [Produces("application/json", "text/xml")] // definisce il formato della richiesta (body, header, etc...)
        public async Task<ActionResult<object>> GetPizze()
        {
            var list = await _ctx.Pizze.AsNoTracking().ToListAsync();
            // wrapper come nell'esempio Java { "pizze": [...] }
            return Ok(new { pizze = list });
        }

        // GET: /api/pizze/{id} (JSON o XML in base all'header Accept)
        [HttpGet("{id:int}")] // Prende l'id endpoint (quindi aggiunge /id al prefisso /api/pizze)
        [Produces("application/json", "text/xml")]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            // fa un match dei dati all'interno di pizza context (quindi della lista delle pizze)
            var pizza = await _ctx.Pizze.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (pizza is null) return NotFound();
            return Ok(pizza);
        }

        // GET: /api/pizze/search
        [HttpGet("search")]
        // Possiamo vedere le query (input) che swagger prende dai argomenti della funzione
        // Il punto interrogativo indica che il valore può essere nullo
        public async Task<IActionResult> Search([FromQuery] decimal? maxPrezzo, [FromQuery] string? categoria)
        {
            // Fa una search query del prezzo massimo e della categoria
            var q = _ctx.Pizze.AsNoTracking().AsQueryable();
            if (maxPrezzo.HasValue) q = q.Where(p => p.Prezzo <= maxPrezzo.Value);
            if (categoria != null) q = q.Where(p => p.Categoria == categoria);
            return Ok(new { pizze = await q.ToListAsync() });
        }


        // DELETE: /api/pizze/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _ctx.Pizze.FindAsync(id);
            if (pizza is null) return NotFound();
            _ctx.Pizze.Remove(pizza);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // PUT: /api/pizze/{id}  Body: { "prezzo": 4.70 }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePrezzo(int id, [FromBody] PrezzoUpdate body)
        {
            // Il metodo Put aggiorna i valori, quindi trova e prende l'oggetto Pizza con l'id corrispondente
            var pizza = await _ctx.Pizze.FindAsync(id);
            // Se non esiste, allora ritorna NotFound
            if (pizza is null) return NotFound();
            // e aggiorna il prezzo
            pizza.Prezzo = body.Prezzo;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // POST: /api/pizze  Body: { "id":4, "nome":"della casa", "prezzo": 8.50 }
        [HttpPost]
        // Prende l'oggetto Pizza dal body (chiamato p)
        public async Task<IActionResult> AddPizza([FromBody] Pizza p)
        {
            // Se esiste (cerca se l'id esiste già)
            var exists = await _ctx.Pizze.AnyAsync(x => x.Id == p.Id);
            // Allora ritorna una bad request
            if (exists) return BadRequest("ID già esistente.");
            // Se il nome della pizza non è dato, allora ritorna una bad request
            if (p.Nome == "") return BadRequest("Nome obbligatorio");
            // Se tutto va, allora aggiunge la pizza alla lista delle pizze.
            _ctx.Pizze.Add(p);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPizzaById), new { id = p.Id }, p);
        }

        public class PrezzoUpdate { public decimal Prezzo { get; set; } }
    }
}
