using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ProductDtos
{
    public class ProductTemplateReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Src { get; set; }
        public int? Location { get; set; }

    }
}
