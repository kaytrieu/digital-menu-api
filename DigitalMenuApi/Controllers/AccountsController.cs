using AutoMapper;
using DigitalMenuApi.Dtos.AccountDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public IActionResult GetAccount(int page, int limit)
        {
            IEnumerable<Account> accounts = _repository.GetAll(page, limit, x => x.IsAvailable == true, x => x.Role, x => x.Store);

            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accounts));
        }


        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public ActionResult<AccountReadDto> GetAccount(int id)
        {
            Account account = _repository.Get(x => x.Id == id && x.IsAvailable == true, x => x.Role, x => x.Store);

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

        // POST: api/Accounts
        [HttpPost("login.json")]
        public IActionResult Login([FromBody] AccountLoginDto loginJson)
        {
            Account accountModel = _repository.Get(x => x.Username == loginJson.Username && x.Password == loginJson.Password, x => x.Role);
            if (accountModel == null)
            {

                return Ok("Invalid username or password");
            }

            string tokenStr = GenerateJSONWebToken(accountModel);

            return Ok(new { token = tokenStr });

        }

        private string GenerateJSONWebToken(Account accountModel)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Role, accountModel.Role.Name),
                new Claim(ClaimTypes.Name, accountModel.Username)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials

                );

            string encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodeToken;

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
