using AutoMapper;
using DigitalMenuApi.Dtos.ScreenTemplateDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class ScreenTemplateProfile : Profile
    {
        public ScreenTemplateProfile()
        {
            //Source to Target
            CreateMap<ScreenTemplate, ScreenTemplateReadDto>();
            CreateMap<ScreenTemplateUpdateDto, ScreenTemplate>();
            CreateMap<ScreenTemplate, ScreenTemplateUpdateDto>();
            CreateMap<ScreenTemplateCreateDto, ScreenTemplate>();
        }

    }
}
