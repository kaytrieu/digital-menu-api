using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public ScreenController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/Screen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Screen>>> GetScreen()
        {
            return await _context.Screen.ToListAsync();
        }

        // GET: api/Screen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Screen>> GetScreen(int id)
        {
            var screen = await _context.Screen.FindAsync(id);

            if (screen == null)
            {
                return NotFound();
            }

            return screen;
        }

        // PUT: api/Screen/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreen(int id, Screen screen)
        {
            if (id != screen.Id)
            {
                return BadRequest();
            }

            _context.Entry(screen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenExists(id))
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

        // POST: api/Screen
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Screen>> PostScreen(Screen screen)
        {
            _context.Screen.Add(screen);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScreenExists(screen.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScreen", new { id = screen.Id }, screen);
        }

        // DELETE: api/Screen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Screen>> DeleteScreen(int id)
        {
            var screen = await _context.Screen.FindAsync(id);
            if (screen == null)
            {
                return NotFound();
            }

            _context.Screen.Remove(screen);
            await _context.SaveChangesAsync();

            return screen;
        }

        private bool ScreenExists(int id)
        {
            return _context.Screen.Any(e => e.Id == id);
        }
    }
}
