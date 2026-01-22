using Microsoft.AspNetCore.Mvc;
using PizzaExpress.Api;
using PizzaExpress.Models;
using System;

namespace PizzaExpress.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzeController : ControllerBase
    {
        private readonly PizzaRepository _repository;

        public PizzeController(PizzaRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pizza = _repository.GetById(id);
            return pizza == null ? NotFound() : Ok(pizza);
        }

        [HttpPost]
        public IActionResult Add(Pizza pizza)
        {
            try
            {
                _repository.Add(pizza);
                return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
            }
            catch (Exception ex)
            {
                // NOTE: Risposta pulita per la GUI
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            try
            {
                pizza.Id = id;

                bool ok = _repository.Update(pizza);
                return ok ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                // NOTE: Risposta pulita
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool ok = _repository.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}