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
    public class TemplateController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public TemplateController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/Template
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Template>>> GetTemplate()
        {
            return await _context.Template.ToListAsync();
        }

        // GET: api/Template/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetTemplate(int id)
        {
            var template = await _context.Template.FindAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return template;
        }

        // PUT: api/Template/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemplate(int id, Template template)
        {
            if (id != template.Id)
            {
                return BadRequest();
            }

            _context.Entry(template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
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

        // POST: api/Template
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Template>> PostTemplate(Template template)
        {
            _context.Template.Add(template);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TemplateExists(template.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTemplate", new { id = template.Id }, template);
        }

        // DELETE: api/Template/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Template>> DeleteTemplate(int id)
        {
            var template = await _context.Template.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Template.Remove(template);
            await _context.SaveChangesAsync();

            return template;
        }

        private bool TemplateExists(int id)
        {
            return _context.Template.Any(e => e.Id == id);
        }
    }
}
