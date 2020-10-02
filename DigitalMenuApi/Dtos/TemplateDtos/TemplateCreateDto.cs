using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateCreateDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public string Uilink { get; set; }
        public string Name { get; set; }
    }
}
