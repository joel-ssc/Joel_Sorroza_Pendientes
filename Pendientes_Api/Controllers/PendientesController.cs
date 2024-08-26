using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pendientes_Api.ContextBD;
using Pendientes_Api.Models;

namespace Pendientes_Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PendientesController: ControllerBase
    {
        private readonly PendienteContext _context;

        public PendientesController(PendienteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pendientes>>> GetPendientes()
        {
            if (_context.Pendientes == null)
            {
                return NotFound();
            }
            return await _context.Pendientes.ToListAsync();
        }

        // GET: api/Pendientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pendientes>> GetPendientes(int id)
        {
            if (_context.Pendientes == null)
            {
                return NotFound();
            }
            var pendientes = await _context.Pendientes.FindAsync(id);

            if (pendientes == null)
            {
                return NotFound();
            }

            return pendientes;
        }

        // PUT: api/Empleado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Pendientes pendientes)
        {
            if (id != pendientes.ID)
            {
                return BadRequest();
            }

            _context.Entry(pendientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool PendienteExists(int id)
        {
            return (_context.Pendientes?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        // POST: api/Empleado
        [HttpPost]
        public async Task<ActionResult<Pendientes>> PostEmpleado(Pendientes pendientes)
        {
            if (_context.Pendientes == null)
            {
                return Problem("Entity set 'PendientesContext.Pendientes'  is null.");
            }
            _context.Pendientes.Add(pendientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPendientes", new { id = pendientes.ID }, pendientes);
        }

        // DELETE: api/Empleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            if (_context.Pendientes == null)
            {
                return NotFound();
            }
            var empleado = await _context.Pendientes.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Pendientes.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
