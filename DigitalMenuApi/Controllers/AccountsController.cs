using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository.Implement;
using DigitalMenuApi.Repository;
using AutoMapper;
using DigitalMenuApi.Dtos;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        public ActionResult GetAccount()
        {
            IEnumerable<Account> accounts = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts));
            //return Ok(accounts);
        }


        //    // GET: api/Accounts/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Account>> GetAccount(int id)
        //    {
        //        var account = await _context.Account.FindAsync(id);

        //        if (account == null)
        //        {
        //            return NotFound();
        //        }

        //        return account;
        //    }

        //    // PUT: api/Accounts/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for
        //    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutAccount(int id, Account account)
        //    {
        //        if (id != account.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(account).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AccountExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Accounts
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for
        //    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //    [HttpPost]
        //    public async Task<ActionResult<Account>> PostAccount(Account account)
        //    {
        //        _context.Account.Add(account);
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException)
        //        {
        //            if (AccountExists(account.Id))
        //            {
        //                return Conflict();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        //    }

        //    // DELETE: api/Accounts/5
        //    [HttpDelete("{id}")]
        //    public async Task<ActionResult<Account>> DeleteAccount(int id)
        //    {
        //        var account = await _context.Account.FindAsync(id);
        //        if (account == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Account.Remove(account);
        //        await _context.SaveChangesAsync();

        //        return account;
        //    }

        //    private bool AccountExists(int id)
        //    {
        //        return _context.Account.Any(e => e.Id == id);
        //    }
    }
}
