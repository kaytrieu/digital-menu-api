using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Service
{
    public interface ITemplateService : IBaseService<Template>
    {
        public Template CreateNewTemplate(TemplateCreateDto templateCreateDto, string uploadedFileLink);
        public bool UpdateTemplateDetail(int templateId, TemplateUpdateDto templateUpdateDto);
        public int GetTemplateIdFromUDID(string udid);
    }
}
