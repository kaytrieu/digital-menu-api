using DigitalMenuApi.Dtos.BoxDtos;
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateUpdateDto
    {
        public TemplateUpdateDto()
        {
            LastModified = DateTime.UtcNow.AddHours(7);

        }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Src { get; set; }
        public DateTime LastModified { get; }
        public ICollection<BoxUpdateWithTemplateDto> Boxes { get; set; }
    }
}
