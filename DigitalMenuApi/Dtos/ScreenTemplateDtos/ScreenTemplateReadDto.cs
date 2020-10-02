using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.ScreenTemplateDtos
{
    public class ScreenTemplateReadDto
    {
        public int Id { get; set; }
        public int ScreenId { get; set; }
        public int TemplateId { get; set; }
        
    }
}
