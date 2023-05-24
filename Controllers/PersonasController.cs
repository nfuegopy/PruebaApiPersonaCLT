using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PersonasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly PersonasDbContext _dbContext;

        public PersonasController(PersonasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Personas
        [HttpGet]
        public IActionResult GetPersonas()
        {
            var personas = _dbContext.Personas?.ToList() ?? new List<Persona>();
            return Ok(personas);
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public IActionResult GetPersona(int id)
        {
            var persona = _dbContext.Personas?.FirstOrDefault(p => p.Id == id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        // POST: api/Personas
        [HttpPost]
        public IActionResult CreatePersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar duplicidad de número de documento
            var duplicado = _dbContext.Personas?.Any(p => p.NumeroDocumento == persona.NumeroDocumento) ?? false;
            if (duplicado)
            {
                ModelState.AddModelError("NumeroDocumento", "Ya existe una persona con este número de documento");
                return BadRequest(ModelState);
            }

            _dbContext.Personas?.Add(persona);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

   // PUT: api/Personas/5
[HttpPut("{id}")]
public IActionResult UpdatePersona(int? id, [FromBody] Persona persona)
{
    if (id == null || id != persona.Id)
    {
        return BadRequest();
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    try
    {
        var personaExistente = _dbContext.Personas?.FirstOrDefault(p => p.Id == id);
        if (personaExistente == null)
        {
            return NotFound();
        }

        // Validar duplicidad de número de documento
        var duplicado = _dbContext.Personas?.Any(p => p.Id != id && p.NumeroDocumento == persona.NumeroDocumento) ?? false;
        if (duplicado)
        {
            ModelState.AddModelError("NumeroDocumento", "Ya existe una persona con este número de documento");
            return BadRequest(ModelState);
        }

        // Actualizar los campos de la persona existente
        personaExistente.Nombre = persona.Nombre;
        personaExistente.Apellido = persona.Apellido;
        personaExistente.NumeroDocumento = persona.NumeroDocumento;
        personaExistente.CorreoElectronico = persona.CorreoElectronico;
        personaExistente.Telefono = persona.Telefono;
        personaExistente.FechaNacimiento = persona.FechaNacimiento;

        _dbContext.SaveChanges();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (_dbContext.Personas?.Any(p => p.Id == id) != true)
        {
            return NotFound();
        }
        throw;
    }

    return NoContent();
}

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public IActionResult DeletePersona(int id)
        {
            var persona = _dbContext.Personas?.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            _dbContext.Personas?.Remove(persona);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
