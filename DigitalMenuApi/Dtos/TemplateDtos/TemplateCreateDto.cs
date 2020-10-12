using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateCreateDto
    {
        public TemplateCreateDto()
        {
            CreatedTime = DateTime.UtcNow.AddHours(7);
        }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public DateTime CreatedTime { get; }
        public string Uilink { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
    }
}
