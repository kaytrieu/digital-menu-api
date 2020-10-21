using AutoMapper;
using DigitalMenuApi.Dtos.AccountRoleDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/account-roles")]
    [ApiController]
    public class AccountRolesController : ControllerBase
    {
        private readonly IAccountRoleRepository _repository;
        private readonly IMapper _mapper;

        public AccountRolesController(IAccountRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/AccountRoles
        [HttpGet]
        public IActionResult GetAccountRole(int page, int limit)
        {
            IEnumerable<AccountRole> AccountRoles = _repository.GetAll(page, limit, x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<AccountRoleReadDto>>(AccountRoles));
            //return Ok(AccountRoles);
        }


        // GET: api/AccountRoles/5
        [HttpGet("{id}")]
        public ActionResult<AccountRoleReadDto> GetAccountRole(int id)
        {
            AccountRole AccountRole = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (AccountRole == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountRoleReadDto>(AccountRole));
        }

        // PUT: api/AccountRoles/5
        [HttpPut("{id}")]
        public IActionResult PutAccountRole(int id, AccountRoleUpdateDto AccountRoleUpdateDto)
        {
            AccountRole AccountRoleFromRepo = _repository.Get(x => x.Id == id);

            if (AccountRoleFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(AccountRoleUpdateDto, AccountRoleFromRepo);

            _repository.Update(AccountRoleFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/AccountRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostAccountRole(AccountRoleCreateDto AccountRoleCreateDto)
        {
            AccountRole AccountRoleModel = _mapper.Map<AccountRole>(AccountRoleCreateDto);

            _repository.Add(AccountRoleModel);
            _repository.SaveChanges();

            AccountRoleReadDto AccountRoleReadDto = _mapper.Map<AccountRoleReadDto>(AccountRoleModel);

            return CreatedAtAction("GetAccountRole", new { id = AccountRoleReadDto.Id }, AccountRoleCreateDto);

        }

        // DELETE: api/AccountRoles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAccountRole(int id)
        {
            AccountRole AccountRoleFromRepo = _repository.Get(x => x.Id == id);

            if (AccountRoleFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(AccountRoleFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchAccountRole(int id, JsonPatchDocument<AccountRoleUpdateDto> patchDoc)
        {
            var AccountRoleModelFromRepo = _repository.Get(x => x.Id == id);

            if (AccountRoleModelFromRepo == null)
            {
                return NotFound();
            }

            var AccountRoleToPatch = _mapper.Map<AccountRoleUpdateDto>(AccountRoleModelFromRepo);

            patchDoc.ApplyTo(AccountRoleToPatch, ModelState);

            if (!TryValidateModel(AccountRoleToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(AccountRoleToPatch, AccountRoleModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(AccountRoleModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
