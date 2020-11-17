using AutoMapper;
using BCrypt.Net;
using DigitalMenuApi.Dtos.AuthenticationDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthenticationController(IAccountRepository repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        public ActionResult<AuthenticationReadDto> Login([FromBody] AuthenticationPostDto loginJson)
        {
            Account accountModel = _repository.Get(x => x.Username == loginJson.Username, x => x.Role, x => x.Store);

            if (accountModel == null)
            {
                return Ok("Invalid username or password");
            }

            bool isAuthorized = accountModel.Password.Equals(BCrypt.Net.BCrypt.HashPassword(loginJson.Password, accountModel.Salt));

            if (!isAuthorized)
            {
                return Ok("Invalid username or password | =" + accountModel.Password + "= | =" + BCrypt.Net.BCrypt.HashPassword(loginJson.Password, accountModel.Salt) + "=");
            }

            string tokenStr = GenerateJSONWebToken(accountModel);

            AuthenticationReadDto authenticationReadDto = _mapper.Map<AuthenticationReadDto>(accountModel);
            authenticationReadDto.Token = tokenStr;

            return Ok(authenticationReadDto);

        }

        private string GenerateJSONWebToken(Account accountModel)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Role, accountModel.Role.Name),
                new Claim(ClaimTypes.Name, accountModel.Username),
                new Claim("accountId", accountModel.Id.ToString())
            };

            if (accountModel.StoreId != null)
            {
                claims = new[]
            {
                new Claim(ClaimTypes.Role, accountModel.Role.Name),
                new Claim(ClaimTypes.Name, accountModel.Username),
                new Claim("storeId",accountModel.StoreId.ToString()),
                new Claim("accountId",accountModel.Id.ToString())
            };
            }

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: null,
                signingCredentials: credentials

                );

            string encodeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodeToken;

        }
    }
}
