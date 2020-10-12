﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplateUpdateDto
    {
        public TemplateUpdateDto()
        {
            LastModified = DateTime.UtcNow.AddHours(7);

        }
        public int Id { get; set; }
        public string Description { get; set; }
        public int? StoreId { get; set; }
        public string Uilink { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public DateTime LastModified { get; }
    }
}
