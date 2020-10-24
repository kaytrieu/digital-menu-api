using DigitalMenuApi.Dtos.ProductListProductDtos;
using System.Collections;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.ProductListDtos
{
    public class ProductListCreateWithTemplateDto
    {
        public string Title { get; set; }
        public int? MaxSize { get; set; }
        public int? Location { get; set; }
        public ICollection<ProductListProductCreateWithTemplateDto> Products { get; set; }
    }
}
