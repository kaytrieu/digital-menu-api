using System;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Uilink { get; set; }
        public string Src { get; set; }

        public string Tags { get; set; }
    }
}
