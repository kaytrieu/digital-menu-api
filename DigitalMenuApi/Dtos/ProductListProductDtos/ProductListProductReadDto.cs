using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ProductListProductDtos
{
    public class ProductListProductReadDto
    {
        public int Id { get; set; }
        public int? ProductListId { get; set; }
        public int? ProductId { get; set; }
        public int? Location { get; set; }
    }
}
