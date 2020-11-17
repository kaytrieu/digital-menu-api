using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.AccountDtos;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using DigitalMenuApi.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static DigitalMenuApi.Models.Extensions.Extensions;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountsController(IAccountRepository repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }

        // GET: api/Accounts
        [HttpGet]
        [AuthorizeRoles(Role.SuperAdmin)]
        public ActionResult<PagingResponseDto<AccountReadDto>> GetAccount(int page = 1, int limit = 10, string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

            PagingDto<Account> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true && x.Username.Contains(searchValue), x => x.Role, x => x.Store);

            var accounts = _mapper.Map<IEnumerable<AccountReadDto>>(dto.Result);

            var response = new PagingResponseDto<AccountReadDto> { Result = accounts, Count = dto.Count };

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


        // GET: api/Accounts/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<AccountReadDto> GetAccount(int id)
        {
            //staff lấy của nó
            //store lấy của store
            //super lấy hết

            Account account = _repository.Get(x => x.Id == id && x.IsAvailable == true, x => x.Role, x => x.Store);

            if (account == null)
            {
                return NotFound();
            }

            //var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            //var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            //if (role == Role.Admin)
            //{
            //    var storeId = claims.Where(x => x.Type == "storeId").FirstOrDefault().Value;

            //    if (!(account.RoleId == RoleId.Staff && account.StoreId == int.Parse(storeId)))
            //    {
            //        return Forbid("You can only get a store's account");
            //    }
            //}

            //if (role == Role.Staff)
            //{
            //    var accountId = claims.Where(x => x.Type == "accountId").FirstOrDefault().Value;

            //    if (account.Id != int.Parse(accountId))
            //    {
            //        return Forbid("You can only get your account");
            //    }
            //}

            

            return Ok(_mapper.Map<AccountReadDto>(account));
        }

        // PUT: api/Accounts/5
        //[AuthorizeRoles(Role.SuperAdmin)]
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutAccount(int id, AccountUpdateDto accountUpdateDto)
        {
            //var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            //var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            //if (role == Role.Admin)
            //{
            //    if (!(accountUpdateDto.RoleId == RoleId.Staff || accountUpdateDto.RoleId == RoleId.Admin))
            //    {
            //        return Forbid("You can only update a store's account");
            //    }
            //}

            //if (role == Role.SuperAdmin)
            //{
            //    if ((accountUpdateDto.RoleId == RoleId.Staff))
            //    {
            //        return Forbid("You can only update a store admin's account");
            //    }
            //}

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
        //store tạo staff, super tạo store
        [HttpPost]
        [AuthorizeRoles(Role.Admin,Role.SuperAdmin)]
        public ActionResult<AccountReadDto> PostAccount(AccountCreateDto accountCreateDto)
        {
            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            if (role == Role.Admin)
            {
                if(!(accountCreateDto.RoleId == RoleId.Staff))
                {
                    return Forbid("You can only create a staff's account");
                }
            }

            if (role == Role.SuperAdmin)
            {
                if ((accountCreateDto.RoleId == RoleId.Staff))
                {
                    return Forbid("You can only create a store admin's account");
                }
            }

            Account accountModel = _mapper.Map<Account>(accountCreateDto);

            _repository.Add(accountModel);

            try
            {
                _repository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.ToString().Contains("duplicate"))
                {
                    return Conflict("Existed Username");
                }
            }
            accountModel = _repository.Get(x => x.Id == accountModel.Id, x => x.Role, x => x.Store);

            AccountReadDto accountReadDto = _mapper.Map<AccountReadDto>(accountModel);

            return CreatedAtAction("GetAccount", new { id = accountReadDto.Id }, accountReadDto);

        }

        //store xóa staff, super xóa store
        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.Admin, Role.SuperAdmin)]
        public IActionResult DeleteAccount(int id)
        {

            Account accountFromRepo = _repository.Get(x => x.Id == id);

            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            if (role == Role.Admin)
            {
                if (!(accountFromRepo.RoleId == RoleId.Staff))
                {
                    return Forbid("You can only delete a staff's account");
                }
            }

            if (role == Role.SuperAdmin)
            {
                if ((accountFromRepo.RoleId == RoleId.Staff))
                {
                    return Forbid("You can only delete a store admin's account");
                }
            }

            if (accountFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(accountFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //store xóa staff, super xóa store
        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchAccount(int id, JsonPatchDocument<AccountUpdateDto> patchDoc)
        {
            Account accountModelFromRepo = _repository.Get(x => x.Id == id);

            if (accountModelFromRepo == null)
            {
                return NotFound();
            }

            AccountUpdateDto accountToPatch = _mapper.Map<AccountUpdateDto>(accountModelFromRepo);

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
