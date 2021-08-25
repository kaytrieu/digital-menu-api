using DigitalMenuApi.Dtos.DesignBoxDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.LayoutDtos
{
    public class LayoutReadDto
    {
        public int Id { get; set; }
        public string AspectRatio { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DesignBoxReadWithLayoutDto> DesignBox { get; set; }
    }
}
