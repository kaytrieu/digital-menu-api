using AutoMapper;
using DigitalMenuApi.Dtos.AccountDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetAccount()
        {
            IEnumerable<Account> accounts = _repository.GetAll(x => x.Role, x => x.Store);
            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts));
            //return Ok(accounts);
        }


        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public ActionResult<AccountReadDto> GetAccount(int id)
        {
            Account account = _repository.Get(x => x.Id == id, x => x.Role, x => x.Store);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountReadDto>(account));
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public IActionResult PutAccount(int id, AccountUpdateDto accountUpdateDto)
        {
            Account accountFromRepo = _repository.Get(x => x.Id == id, x => x.Role, x => x.Store);

            if (accountFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(accountUpdateDto, accountFromRepo);

            _repository.Update(accountFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/Accounts
        [HttpPost]
        public IActionResult PostAccount(AccountCreateDto accountCreateDto)
        {
            Account accountModel = _mapper.Map<Account>(accountCreateDto);

            _repository.Add(accountModel);
            _repository.SaveChanges();

            AccountReadDto accountReadDto = _mapper.Map<AccountReadDto>(accountModel);

            return CreatedAtAction("GetAccount", new { id = accountReadDto.Id }, accountCreateDto);

        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            Account accountFromRepo = _repository.Get(x => x.Id == id);

            if (accountFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(accountFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchAccount(int id, JsonPatchDocument<AccountUpdateDto> patchDoc)
        {
            var accountModelFromRepo = _repository.Get(x => x.Id == id);

            if (accountModelFromRepo == null)
            {
                return NotFound();
            }

            var accountToPatch = _mapper.Map<AccountUpdateDto>(accountModelFromRepo);

            patchDoc.ApplyTo(accountToPatch, ModelState);

            if (!TryValidateModel(accountToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(accountToPatch, accountModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(accountModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }


    }
}
