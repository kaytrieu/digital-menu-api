using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.Dtos.ProductListProductDtos;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.BoxDtos
{
    public class BoxCreateWithTemplateDto
    {
        public int? Location { get; set; }
        public int? BoxTypeId { get; set; }
        public string Src { get; set; }
        public string HeaderTitle { get; set; }
        public string FooterTitle { get; set; }
        public string HeaderSrc { get; set; }
        public string FooterSrc { get; set; }
        public ICollection<ProductListCreateWithTemplateDto> ProductLists { get; set; }


    }
}
