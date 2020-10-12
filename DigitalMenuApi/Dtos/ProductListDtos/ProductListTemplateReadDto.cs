using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ProductListDtos
{
    public class ProductListTemplateReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? BoxId { get; set; }
        public int? MaxSize { get; set; }
        public int? Location { get; set; }
        public ICollection<ProductTemplateReadDto> Products { get; set; }


    }
}
