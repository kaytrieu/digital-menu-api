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
    public class BoxTypeController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public BoxTypeController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/BoxType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoxType>>> GetBoxType()
        {
            return await _context.BoxType.ToListAsync();
        }

        // GET: api/BoxType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoxType>> GetBoxType(int id)
        {
            var boxType = await _context.BoxType.FindAsync(id);

            if (boxType == null)
            {
                return NotFound();
            }

            return boxType;
        }

        // PUT: api/BoxType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoxType(int id, BoxType boxType)
        {
            if (id != boxType.Id)
            {
                return BadRequest();
            }

            _context.Entry(boxType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoxTypeExists(id))
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

        // POST: api/BoxType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BoxType>> PostBoxType(BoxType boxType)
        {
            _context.BoxType.Add(boxType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BoxTypeExists(boxType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBoxType", new { id = boxType.Id }, boxType);
        }

        // DELETE: api/BoxType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BoxType>> DeleteBoxType(int id)
        {
            var boxType = await _context.BoxType.FindAsync(id);
            if (boxType == null)
            {
                return NotFound();
            }

            _context.BoxType.Remove(boxType);
            await _context.SaveChangesAsync();

            return boxType;
        }

        private bool BoxTypeExists(int id)
        {
            return _context.BoxType.Any(e => e.Id == id);
        }
    }
}
