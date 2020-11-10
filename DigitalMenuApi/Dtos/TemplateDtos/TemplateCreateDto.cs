using DigitalMenuApi.Dtos.BoxDtos;
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateCreateDto
    {
        public TemplateCreateDto()
        {
            CreatedTime = DateTime.UtcNow.AddHours(7);
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public string Tags { get; set; }
        public string Src { get; set; }

        public DateTime CreatedTime { get;}
        public ICollection<BoxCreateWithTemplateDto> Boxes { get; set; }
    }
}
