using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ProductListDtos
{
    public class ProductListCreateDto
    {
        public string Title { get; set; }
        public int? BoxId { get; set; }
        public int? MaxSize { get; set; }
    }
}
