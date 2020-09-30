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
    public class BoxesController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public BoxesController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/Boxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Box>>> GetBox()
        {
            return await _context.Box.ToListAsync();
        }

        // GET: api/Boxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBox(int id)
        {
            var box = await _context.Box.FindAsync(id);

            if (box == null)
            {
                return NotFound();
            }

            return box;
        }

        // PUT: api/Boxes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBox(int id, Box box)
        {
            if (id != box.Id)
            {
                return BadRequest();
            }

            _context.Entry(box).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxExists(id))
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

        // POST: api/Boxes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Box>> PostBox(Box box)
        {
            _context.Box.Add(box);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BoxExists(box.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBox", new { id = box.Id }, box);
        }

        // DELETE: api/Boxes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Box>> DeleteBox(int id)
        {
            var box = await _context.Box.FindAsync(id);
            if (box == null)
            {
                return NotFound();
            }

            _context.Box.Remove(box);
            await _context.SaveChangesAsync();

            return box;
        }

        private bool BoxExists(int id)
        {
            return _context.Box.Any(e => e.Id == id);
        }
    }
}
