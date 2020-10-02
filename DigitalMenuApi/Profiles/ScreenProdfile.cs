using AutoMapper;
using DigitalMenuApi.Dtos.ScreenDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class ScreenProfile : Profile
    {
        public ScreenProfile()
        {
            //Source to Target
            CreateMap<Screen, ScreenReadDto>();
            CreateMap<ScreenUpdateDto, Screen>();
            CreateMap<Screen, ScreenUpdateDto>();
            CreateMap<ScreenCreateDto, Screen>();
        }

    }
}
