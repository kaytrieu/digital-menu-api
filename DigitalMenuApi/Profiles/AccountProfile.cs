using AutoMapper;
using DigitalMenuApi.Dtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            //Source to Target
            CreateMap<Account, AccountReadDto>();
                //.ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                //.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }
        
    }
}
