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
    public class AccountRolesController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public AccountRolesController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/AccountRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountRole>>> GetAccountRole()
        {
            return await _context.AccountRole.ToListAsync();
        }

        // GET: api/AccountRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountRole>> GetAccountRole(int id)
        {
            var AccountRole = await _context.AccountRole.FindAsync(id);

            if (AccountRole == null)
            {
                return NotFound();
            }

            return AccountRole;
        }

        // PUT: api/AccountRoles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountRole(int id, AccountRole AccountRole)
        {
            if (id != AccountRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(AccountRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountRoleExists(id))
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

        // POST: api/AccountRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AccountRole>> PostAccountRole(AccountRole AccountRole)
        {
            _context.AccountRole.Add(AccountRole);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountRoleExists(AccountRole.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountRole", new { id = AccountRole.Id }, AccountRole);
        }

        // DELETE: api/AccountRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountRole>> DeleteAccountRole(int id)
        {
            var AccountRole = await _context.AccountRole.FindAsync(id);
            if (AccountRole == null)
            {
                return NotFound();
            }

            _context.AccountRole.Remove(AccountRole);
            await _context.SaveChangesAsync();

            return AccountRole;
        }

        private bool AccountRoleExists(int id)
        {
            return _context.AccountRole.Any(e => e.Id == id);
        }
    }
}
