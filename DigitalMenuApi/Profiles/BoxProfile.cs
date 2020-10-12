using AutoMapper;
using DigitalMenuApi.Dtos.BoxDtos;
using DigitalMenuApi.Dtos.ProductListProductDtos;
using DigitalMenuApi.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace DigitalMenuApi.Profiles
{
    public class BoxProfile : Profile
    {
        public BoxProfile()
        {
            //Source to Target
            CreateMap<Box, BoxReadDto>();
            CreateMap<Box, BoxDetailTemplateReadDto>().ForMember(dest => dest.ProductLists, opt => opt.MapFrom(src => src.ProductList));
            CreateMap<BoxUpdateDto, Box>();
            CreateMap<Box, BoxUpdateDto>();
            CreateMap<BoxCreateDto, Box>();

        }


    }
}
