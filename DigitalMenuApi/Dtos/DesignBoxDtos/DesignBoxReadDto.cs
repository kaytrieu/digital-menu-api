using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.DesignBoxDtos
{
    public class DesignBoxReadDto
    {
        public int Id { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public int? OffsetX { get; set; }
        public int? OffsetY { get; set; }
        public int? LayoutId { get; set; }
    }
}
