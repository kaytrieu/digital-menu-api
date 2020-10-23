using DigitalMenuApi.Dtos.BoxTypeDtos;
using DigitalMenuApi.Dtos.ProductListDtos;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.BoxDtos
{
    public class BoxDetailTemplateReadDto
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public BoxTypeReadDto BoxType { get; set; }

        public int? Location { get; set; }
        public string Src { get; set; }
        public string HeaderTitle { get; set; }
        public string FooterTitle { get; set; }
        public string HeaderSrc { get; set; }
        public string FooterSrc { get; set; }
        public ICollection<ProductListTemplateReadDto> ProductLists { get; set; }


    }
}
