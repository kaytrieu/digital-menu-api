using DigitalMenuApi.Dtos.BoxDtos;
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateDetailReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Uilink { get; set; }
        public string Src { get; set; }

        public ICollection<BoxDetailTemplateReadDto> Boxes { get; set; }

    }
}
