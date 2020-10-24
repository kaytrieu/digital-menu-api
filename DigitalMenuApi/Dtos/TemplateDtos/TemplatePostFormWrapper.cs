using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.TemplateDtos
{
    public class TemplatePostFormWrapper
    {
        public IFormFile file { get; set; }
        public string templateDtoJson { get; set; }
    }
}
