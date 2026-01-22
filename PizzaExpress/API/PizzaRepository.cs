using PizzaExpress.Models;
using PizzaExpress.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PizzaExpress.Api
{
    public class PizzaRepository
    {
        private readonly PizzaDbContext _context;

        public PizzaRepository(PizzaDbContext context)
        {
            _context = context;
        }

        public List<Pizza> GetAll()
        {
            return _context.Pizze
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Pizza? GetById(int id)
        {
            return _context.Pizze.FirstOrDefault(p => p.Id == id);
        }

        // =====================================================================
        //  AGGIUNTA PIZZA CON RICALCOLO DELL'ID SEQUENZIALE
        // =====================================================================
        public void Add(Pizza pizza)
        {
            // Controllo duplicati nome
            if (_context.Pizze.Any(p => p.Nome.ToLower() == pizza.Nome.ToLower()))
                throw new Exception("Esiste già una pizza con questo nome!");

            // Calcolo del nuovo ID sequenziale corretto
            int nextId = _context.Pizze.Any()
                ? _context.Pizze.Max(p => p.Id) + 1
                : 1;

            pizza.Id = nextId;

            _context.Pizze.Add(pizza);
            _context.SaveChanges();
        }

        // =====================================================================
        //  AGGIORNAMENTO PIZZA
        // =====================================================================
        public bool Update(Pizza pizza)
        {
            var existing = _context.Pizze.FirstOrDefault(p => p.Id == pizza.Id);
            if (existing == null)
                return false;

            // Controllo duplicati nome (escludendo se stesso)
            if (_context.Pizze.Any(p => p.Id != pizza.Id &&
                                        p.Nome.ToLower() == pizza.Nome.ToLower()))
                throw new Exception("Esiste già una pizza con questo nome!");

            existing.Nome = pizza.Nome;
            existing.Prezzo = pizza.Prezzo;
            existing.Categoria = pizza.Categoria;
            existing.Note = pizza.Note;
            existing.Stato = pizza.Stato;

            _context.SaveChanges();
            return true;
        }

        // =====================================================================
        //  CANCELLAZIONE + REINDICIZZAZIONE CORRETTA
        // =====================================================================
        public bool Delete(int id)
        {
            var pizza = _context.Pizze.FirstOrDefault(p => p.Id == id);
            if (pizza == null)
                return false;

            // 1) Rimuovi la pizza
            _context.Pizze.Remove(pizza);
            _context.SaveChanges();

            // 2) Prendi tutte le pizze rimaste ordinate
            var vecchie = _context.Pizze
                .OrderBy(p => p.Id)
                .ToList();

            // 3) Cancella completamente la tabella
            _context.Pizze.RemoveRange(_context.Pizze);
            _context.SaveChanges();

            // 4) Reindicizzazione sequenziale
            int newId = 1;

            foreach (var p in vecchie)
            {
                var nuova = new Pizza
                {
                    Id = newId,
                    Nome = p.Nome,
                    Prezzo = p.Prezzo,
                    Categoria = p.Categoria,
                    Note = p.Note
                };

                _context.Pizze.Add(nuova);
                newId++;
            }

            _context.SaveChanges();
            return true;
        }
    }
}