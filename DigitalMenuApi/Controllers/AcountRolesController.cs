using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.AccountRoleDtos;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using DigitalMenuApi.Models.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static DigitalMenuApi.Models.Extensions.Extensions;

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
        //super admin
        [AuthorizeRoles(Role.SuperAdmin)]
        [HttpGet]
        public ActionResult<PagingResponseDto<AccountRoleReadDto>> GetAccountRole(int page = 1, int limit = 10, string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

            PagingDto<AccountRole> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true && x.Name.Contains(searchValue));

            var accountRoles = _mapper.Map<IEnumerable<AccountRoleReadDto>>(dto.Result);

            var response = new PagingResponseDto<AccountRoleReadDto> { Result = accountRoles, Count = dto.Count };

            if (limit > 0)
            {
                if ((double)dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit, searchValue });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit, searchValue });
            }

            return Ok(response);
        }

        //super admin
        // GET: api/AccountRoles/5
        [AuthorizeRoles(Role.SuperAdmin)]
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

        //superadmin
        // PUT: api/AccountRoles/5
        [AuthorizeRoles(Role.SuperAdmin)]
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

        //superadmin
        // POST: api/AccountRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [AuthorizeRoles(Role.SuperAdmin)]
        [HttpPost]
        public IActionResult PostAccountRole(AccountRoleCreateDto AccountRoleCreateDto)
        {
            AccountRole AccountRoleModel = _mapper.Map<AccountRole>(AccountRoleCreateDto);

            _repository.Add(AccountRoleModel);
            _repository.SaveChanges();

            AccountRoleReadDto AccountRoleReadDto = _mapper.Map<AccountRoleReadDto>(AccountRoleModel);

            return CreatedAtAction("GetAccountRole", new { id = AccountRoleReadDto.Id }, AccountRoleCreateDto);

        }

        //superadmin
        // DELETE: api/AccountRoles/5
        [AuthorizeRoles(Role.SuperAdmin)]
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

        //superadmin
        //Patch
        [AuthorizeRoles(Role.SuperAdmin)]
        [HttpPatch("{id}")]
        public IActionResult PatchAccountRole(int id, JsonPatchDocument<AccountRoleUpdateDto> patchDoc)
        {
            AccountRole AccountRoleModelFromRepo = _repository.Get(x => x.Id == id);

            if (AccountRoleModelFromRepo == null)
            {
                return NotFound();
            }

            AccountRoleUpdateDto AccountRoleToPatch = _mapper.Map<AccountRoleUpdateDto>(AccountRoleModelFromRepo);

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
