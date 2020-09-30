using AutoMapper;
using DigitalMenuApi.Dtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class AccountRoleProfile : Profile
    {
        public AccountRoleProfile()
        {
            //Source to Target

            CreateMap<AccountRole, AccountRoleReadDto>();
        }
        
    }
}
