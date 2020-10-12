using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Dtos.ProductListDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ProductListProductDtos
{
    public class ProductListProductTemplateReadDto
    {
        public ICollection<ProductReadDto> Product { get; set; }
        public ProductListReadDto ProductList { get; set; }
    }
}
