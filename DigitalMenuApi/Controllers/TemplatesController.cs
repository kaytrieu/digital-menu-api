using AutoMapper;
using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateRepository _repository;
        private readonly IMapper _mapper;

        public TemplatesController(ITemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Templates
        [HttpGet]
        public ActionResult<IEnumerable<TemplateReadDto>> GetTemplate()
        {
            IEnumerable<Template> Templates = _repository.GetAll(x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<TemplateReadDto>>(Templates));
            //return Ok(Templates);
        }


        // GET: api/Templates/5
        [HttpGet("{id}")]
        public ActionResult<TemplateReadDto> GetTemplate(int id)
        {
            Template Template = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (Template == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TemplateReadDto>(Template));
        }

        // PUT: api/Templates/5
        [HttpPut("{id}")]
        public IActionResult PutTemplate(int id, TemplateUpdateDto TemplateUpdateDto)
        {
            Template TemplateFromRepo = _repository.Get(x => x.Id == id);

            if (TemplateFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(TemplateUpdateDto, TemplateFromRepo);

            _repository.Update(TemplateFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/Templates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostTemplate(TemplateCreateDto TemplateCreateDto)
        {
            Template TemplateModel = _mapper.Map<Template>(TemplateCreateDto);

            _repository.Add(TemplateModel);
            _repository.SaveChanges();

            TemplateReadDto TemplateReadDto = _mapper.Map<TemplateReadDto>(TemplateModel);

            return CreatedAtAction("GetTemplate", new { id = TemplateReadDto.Id }, TemplateCreateDto);

        }

        // DELETE: api/Templates/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTemplate(int id)
        {
            Template TemplateFromRepo = _repository.Get(x => x.Id == id);

            if (TemplateFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(TemplateFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchTemplate(int id, JsonPatchDocument<TemplateUpdateDto> patchDoc)
        {
            var TemplateModelFromRepo = _repository.Get(x => x.Id == id);

            if (TemplateModelFromRepo == null)
            {
                return NotFound();
            }

            var TemplateToPatch = _mapper.Map<TemplateUpdateDto>(TemplateModelFromRepo);

            patchDoc.ApplyTo(TemplateToPatch, ModelState);

            if (!TryValidateModel(TemplateToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(TemplateToPatch, TemplateModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(TemplateModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
