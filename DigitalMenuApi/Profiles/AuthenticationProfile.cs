using AutoMapper;
using DigitalMenuApi.Dtos.AuthenticationDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Account, AuthenticationReadDto>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src));

        }

    }
}
