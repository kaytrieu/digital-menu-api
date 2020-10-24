﻿using AutoMapper;
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
            CreateMap<Box, BoxDetailTemplateReadDto>().ForMember(dest => dest.ProductLists, opt => opt.MapFrom(src => src.ProductList));
            CreateMap<BoxUpdateDto, Box>();
            CreateMap<Box, BoxUpdateDto>();
            CreateMap<BoxCreateDto, Box>();
            CreateMap<BoxCreateWithTemplateDto, Box>();

        }


    }
}
