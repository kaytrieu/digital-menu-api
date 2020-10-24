using AutoMapper;
using DigitalMenuApi.Dtos.ProductListProductDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class ProductListProductProfile : Profile
    {
        public ProductListProductProfile()
        {
            //Source to Target
            CreateMap<ProductListProduct, ProductListProductReadDto>();
            CreateMap<ProductListProduct, ProductListProductTemplateReadDto>();
            CreateMap<ProductListProductUpdateDto, ProductListProduct>();
            CreateMap<ProductListProduct, ProductListProductUpdateDto>();
            CreateMap<ProductListProductCreateWithTemplateDto, ProductListProduct>();
        }

    }
}
