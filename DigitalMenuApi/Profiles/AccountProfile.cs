using AutoMapper;
using DigitalMenuApi.Dtos.AccountDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            //Source to Target
            CreateMap<Account, AccountReadDto>();
            CreateMap<Account, AccountReadAfterAuthenDto>();
            CreateMap<AccountUpdateDto, Account>();
            CreateMap<Account, AccountUpdateDto>();
            CreateMap<AccountCreateDto, Account>();
        }

    }
}
