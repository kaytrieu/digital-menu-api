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
            CreateMap<ProductListProductUpdateDto, ProductListProduct>();
            CreateMap<ProductListProduct, ProductListProductUpdateDto>();
            CreateMap<ProductListProductCreateDto, ProductListProduct>();
        }

    }
}
