using DigitalMenuApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateReadDto
    {
        public TemplateReadDto()
        {
            Box = new HashSet<Box>();
            ScreenTemplate = new HashSet<ScreenTemplate>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsAvailable { get; set; }
        public string Uilink { get; set; }
        public string Name { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Box> Box { get; set; }
        public virtual ICollection<ScreenTemplate> ScreenTemplate { get; set; }
    }
}
