using AutoMapper;
using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Profiles
{
    public class ProductListProfile : Profile
    {
        public ProductListProfile()
        {
            //Source to Target
            CreateMap<ProductList, ProductListReadDto>();
            CreateMap<ProductListUpdateDto, ProductList>();
            CreateMap<ProductList, ProductListUpdateDto>();
            CreateMap<ProductListCreateDto, ProductList>();
        }

    }
}
