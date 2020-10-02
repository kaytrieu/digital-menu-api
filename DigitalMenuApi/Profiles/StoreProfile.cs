using AutoMapper;
using DigitalMenuApi.Dtos.StoreDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            //Source to Target
            CreateMap<Store, StoreReadDto>();
            CreateMap<StoreUpdateDto, Store>();
            CreateMap<Store, StoreUpdateDto>();
            CreateMap<StoreCreateDto, Store>();
        }

    }
}
