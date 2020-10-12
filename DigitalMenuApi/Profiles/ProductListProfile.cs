using AutoMapper;
using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.Models;
using System.Linq;

namespace DigitalMenuApi.Profiles
{
    public class ProductListProfile : Profile
    {
        public ProductListProfile()
        {
            //Source to Target
            CreateMap<ProductList, ProductListReadDto>();
            CreateMap<ProductList, ProductListTemplateReadDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductListProduct.Select(x => x.Product).ToList()))
                  .AfterMap((_, dest) 
                    => dest.Products.ToList().ForEach(
                        x => x.Location = _.ProductListProduct.
                                                Where(y => y.ProductId == x.Id).
                                                Select(x => x.Location).FirstOrDefault()));
            CreateMap<ProductListUpdateDto, ProductList>();
            CreateMap<ProductList, ProductListUpdateDto>();
            CreateMap<ProductListCreateDto, ProductList>();
        }

    }
}
