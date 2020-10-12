using AutoMapper;
using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            //Source to Target
            CreateMap<Template, TemplateReadDto>();
            CreateMap<Template, TemplateDetailReadDto>().ForMember(dest => dest.Boxes, opt => opt.MapFrom(src => src.Box));
            CreateMap<TemplateUpdateDto, Template>();
            CreateMap<Template, TemplateUpdateDto>();
            CreateMap<TemplateCreateDto, Template>();
        }

    }
}
