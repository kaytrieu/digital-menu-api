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
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductListProduct.OrderBy(x => x.Location).Select(x => x.Product).ToList()))
                  .AfterMap((_, dest)
                    => dest.Products.ToList().ForEach(
                        x => x.Location = _.ProductListProduct.ElementAt(dest.Products.ToList().IndexOf(x)).Location));
                                                //Select(x => x.Location).ElementAt(dest.Products.ToList().IndexOf(x)).GetValueOrDefault()));
            CreateMap<ProductListUpdateDto, ProductList>();
            CreateMap<ProductList, ProductListUpdateDto>();
            CreateMap<ProductListCreateWithTemplateDto, ProductList>();
        }

    }
}
