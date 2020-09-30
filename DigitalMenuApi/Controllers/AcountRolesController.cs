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
    public class AcountRolesController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public AcountRolesController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/AcountRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcountRole>>> GetAcountRole()
        {
            return await _context.AcountRole.ToListAsync();
        }

        // GET: api/AcountRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AcountRole>> GetAcountRole(int id)
        {
            var acountRole = await _context.AcountRole.FindAsync(id);

            if (acountRole == null)
            {
                return NotFound();
            }

            return acountRole;
        }

        // PUT: api/AcountRoles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcountRole(int id, AcountRole acountRole)
        {
            if (id != acountRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(acountRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcountRoleExists(id))
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

        // POST: api/AcountRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AcountRole>> PostAcountRole(AcountRole acountRole)
        {
            _context.AcountRole.Add(acountRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AcountRoleExists(acountRole.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAcountRole", new { id = acountRole.Id }, acountRole);
        }

        // DELETE: api/AcountRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AcountRole>> DeleteAcountRole(int id)
        {
            var acountRole = await _context.AcountRole.FindAsync(id);
            if (acountRole == null)
            {
                return NotFound();
            }

            _context.AcountRole.Remove(acountRole);
            await _context.SaveChangesAsync();

            return acountRole;
        }

        private bool AcountRoleExists(int id)
        {
            return _context.AcountRole.Any(e => e.Id == id);
        }
    }
}
