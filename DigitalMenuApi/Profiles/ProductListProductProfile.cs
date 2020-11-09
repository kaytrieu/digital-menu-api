using AutoMapper;
using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Dtos.ProductDtos;
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
            CreateMap<ProductListProductTemplateUpdateDto, ProductListProduct>()
                .ForMember(x => x.Location, src => src.MapFrom(src => src.Location))
                .ForMember(x => x.ProductId, src => src.MapFrom(src => src.Id))
                .ForMember(x => x.Id, src => src.Ignore())
                .ForMember(x => x.IsAvailable, src => src.Ignore())
                .ForMember(x => x.Product, src => src.Ignore())
                .ForMember(x => x.ProductListId, src => src.Ignore())
                .ForMember(x => x.ProductList, src => src.Ignore());

        }

    }
}
