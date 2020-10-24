using AutoMapper;
using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using DigitalMenuApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;

        public TemplatesController(ITemplateRepository templateRepository, IMapper mapper, ITemplateService templateService)
        {
            _templateService = templateService;

            _templateRepository = templateRepository;
            _mapper = mapper;
        }

        // GET: api/Templates
        [HttpGet]
        public ActionResult<IEnumerable<TemplateReadDto>> GetTemplate(int page, int limit, string tag = "", string searchValue = "")
        {
            IEnumerable<Template> templates = _templateRepository.GetAll(page, limit, predicate: x => x.IsAvailable == true
                                                                      && x.Tags.ToLower().Contains(tag.ToLower())
                                                                      && x.Name.ToLower().Contains(searchValue.ToLower()));

            return Ok(_mapper.Map<IEnumerable<TemplateReadDto>>(templates));
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public ActionResult<TemplateDetailReadDto> GetDetailTemplate(int id)
        {
            Template templateFromRepo = _templateRepository.Get(x => x.Id == id && x.IsAvailable == true,
                template => template
                .Include(template => template.Box)
                    .ThenInclude(box => box.ProductList)
                        .ThenInclude(productList => productList.ProductListProduct)
                            .ThenInclude(productListProduct => productListProduct.Product)
                .Include(template => template.Box)
                    .ThenInclude(box => box.BoxType));

            if (templateFromRepo == null)
            {
                return NotFound();
            }

            TemplateDetailReadDto dto = _mapper.Map<TemplateDetailReadDto>(templateFromRepo);

            return Ok(dto);
        }

        // PUT: api/Templates/5
        [HttpPut("{id}")]
        public IActionResult PutTemplate(int id, TemplateUpdateDto TemplateUpdateDto)
        {
            Template TemplateFromRepo = _templateRepository.Get(x => x.Id == id);

            if (TemplateFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(TemplateUpdateDto, TemplateFromRepo);

            _templateRepository.Update(TemplateFromRepo);

            _templateRepository.SaveChanges();


            return NoContent();
        }

        // POST: api/Templates
        [HttpPost]
        public IActionResult PostTemplate([FromForm]TemplatePostFormWrapper formWrapper)
        {
            var file = formWrapper.file;
            TemplateCreateDto templateDto = JsonConvert.DeserializeObject<TemplateCreateDto>(formWrapper.templateDto);
            if (file.Length > 0)
            {
                string uploadedFileLink = FirebaseService.UploadFileToFirebaseStorage(file.OpenReadStream(), DateTime.Now.ToString("ddMMyyyyHHmmssff") + file.FileName).Result;

                if (templateDto != null)
                {

                    Template createdTemplate = _templateService.CreateNewTemplate(templateDto, uploadedFileLink);


                    TemplateDetailReadDto TemplateReadDto = _mapper.Map<TemplateDetailReadDto>(createdTemplate);

                    return CreatedAtAction("GetTemplate", new { id = createdTemplate.Id }, TemplateReadDto);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        // DELETE: api/Templates/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTemplate(int id)
        {
            Template TemplateFromRepo = _templateRepository.Get(x => x.Id == id);

            if (TemplateFromRepo == null)
            {
                return NotFound();
            }

            _templateRepository.Delete(TemplateFromRepo);

            _templateRepository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchTemplate(int id, JsonPatchDocument<TemplateUpdateDto> patchDoc)
        {
            Template TemplateModelFromRepo = _templateRepository.Get(x => x.Id == id);

            if (TemplateModelFromRepo == null)
            {
                return NotFound();
            }

            TemplateUpdateDto TemplateToPatch = _mapper.Map<TemplateUpdateDto>(TemplateModelFromRepo);

            patchDoc.ApplyTo(TemplateToPatch, ModelState);

            if (!TryValidateModel(TemplateToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(TemplateToPatch, TemplateModelFromRepo);

            //Temp is not doing nothing
            _templateRepository.Update(TemplateModelFromRepo);

            _templateRepository.SaveChanges();

            return NoContent();
        }
    }
}
