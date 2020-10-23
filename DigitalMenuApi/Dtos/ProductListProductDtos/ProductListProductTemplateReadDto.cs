using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Dtos.ProductListDtos;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.ProductListProductDtos
{
    public class ProductListProductTemplateReadDto
    {
        public ICollection<ProductReadDto> Product { get; set; }
        public ProductListReadDto ProductList { get; set; }
    }
}
