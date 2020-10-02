using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ProductListProductDtos
{
    public class ProductListProductUpdateDto
    {
        public int? ProductListId { get; set; }
        public int? ProductId { get; set; }
        public int? Location { get; set; }
    }
}
