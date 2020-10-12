using AutoMapper;
using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Source to Target
            CreateMap<Product, ProductReadDto>();
            CreateMap<Product, ProductTemplateReadDto>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductCreateDto, Product>();
        }

    }
}
