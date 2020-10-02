using AutoMapper;
using DigitalMenuApi.Dtos.BoxTypeDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class BoxTypeProfile : Profile
    {
        public BoxTypeProfile()
        {
            //Source to Target
            CreateMap<BoxType, BoxTypeReadDto>();
            CreateMap<BoxTypeUpdateDto, BoxType>();
            CreateMap<BoxType, BoxTypeUpdateDto>();
            CreateMap<BoxTypeCreateDto, BoxType>();
        }

    }
}
