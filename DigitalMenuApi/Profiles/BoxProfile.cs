using AutoMapper;
using DigitalMenuApi.Dtos.BoxDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class BoxProfile : Profile
    {
        public BoxProfile()
        {
            //Source to Target
            CreateMap<Box, BoxReadDto>();
            CreateMap<BoxUpdateDto, Box>();
            CreateMap<Box, BoxUpdateDto>();
            CreateMap<BoxCreateDto, Box>();
        }

    }
}
