using AutoMapper;
using DigitalMenuApi.Dtos;
using DigitalMenuApi.Dtos.AccountRoleDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class AccountRoleProfile : Profile
    {
        public AccountRoleProfile()
        {
            //Source to Target
            CreateMap<AccountRole, AccountRoleReadDto>();
            CreateMap<AccountRoleUpdateDto, AccountRole>();
            CreateMap<AccountRole, AccountRoleUpdateDto>();
            CreateMap<AccountRoleCreateDto, AccountRole>();
        }

    }
}
