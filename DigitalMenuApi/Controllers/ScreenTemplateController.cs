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
    public class ScreenTemplateController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public ScreenTemplateController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/ScreenTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenTemplate>>> GetScreenTemplate()
        {
            return await _context.ScreenTemplate.ToListAsync();
        }

        // GET: api/ScreenTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenTemplate>> GetScreenTemplate(int id)
        {
            var screenTemplate = await _context.ScreenTemplate.FindAsync(id);

            if (screenTemplate == null)
            {
                return NotFound();
            }

            return screenTemplate;
        }

        // PUT: api/ScreenTemplate/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreenTemplate(int id, ScreenTemplate screenTemplate)
        {
            if (id != screenTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(screenTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenTemplateExists(id))
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

        // POST: api/ScreenTemplate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ScreenTemplate>> PostScreenTemplate(ScreenTemplate screenTemplate)
        {
            _context.ScreenTemplate.Add(screenTemplate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScreenTemplateExists(screenTemplate.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScreenTemplate", new { id = screenTemplate.Id }, screenTemplate);
        }

        // DELETE: api/ScreenTemplate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScreenTemplate>> DeleteScreenTemplate(int id)
        {
            var screenTemplate = await _context.ScreenTemplate.FindAsync(id);
            if (screenTemplate == null)
            {
                return NotFound();
            }

            _context.ScreenTemplate.Remove(screenTemplate);
            await _context.SaveChangesAsync();

            return screenTemplate;
        }

        private bool ScreenTemplateExists(int id)
        {
            return _context.ScreenTemplate.Any(e => e.Id == id);
        }
    }
}
