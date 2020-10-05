using AutoMapper;
using DigitalMenuApi.Dtos.ScreenTemplateDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/screen-templates")]
    [ApiController]
    public class ScreenTemplatesController : ControllerBase
    {
        private readonly IScreenTemplateRepository _repository;
        private readonly IMapper _mapper;

        public ScreenTemplatesController(IScreenTemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ScreenTemplates
        [HttpGet]
        public IActionResult GetScreenTemplate()
        {
            IEnumerable<ScreenTemplate> ScreenTemplates = _repository.GetAll(x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<ScreenTemplateReadDto>>(ScreenTemplates));
            //return Ok(ScreenTemplates);
        }


        // GET: api/ScreenTemplates/5
        [HttpGet("{id}")]
        public ActionResult<ScreenTemplateReadDto> GetScreenTemplate(int id)
        {
            ScreenTemplate ScreenTemplate = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (ScreenTemplate == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScreenTemplateReadDto>(ScreenTemplate));
        }

        // PUT: api/ScreenTemplates/5
        [HttpPut("{id}")]
        public IActionResult PutScreenTemplate(int id, ScreenTemplateUpdateDto ScreenTemplateUpdateDto)
        {
            ScreenTemplate ScreenTemplateFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenTemplateFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(ScreenTemplateUpdateDto, ScreenTemplateFromRepo);

            _repository.Update(ScreenTemplateFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/ScreenTemplates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostScreenTemplate(ScreenTemplateCreateDto ScreenTemplateCreateDto)
        {
            ScreenTemplate ScreenTemplateModel = _mapper.Map<ScreenTemplate>(ScreenTemplateCreateDto);

            _repository.Add(ScreenTemplateModel);
            _repository.SaveChanges();

            ScreenTemplateReadDto ScreenTemplateReadDto = _mapper.Map<ScreenTemplateReadDto>(ScreenTemplateModel);

            return CreatedAtAction("GetScreenTemplate", new { id = ScreenTemplateReadDto.Id }, ScreenTemplateCreateDto);

        }

        // DELETE: api/ScreenTemplates/5
        [HttpDelete("{id}")]
        public IActionResult DeleteScreenTemplate(int id)
        {
            ScreenTemplate ScreenTemplateFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenTemplateFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(ScreenTemplateFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchScreenTemplate(int id, JsonPatchDocument<ScreenTemplateUpdateDto> patchDoc)
        {
            var ScreenTemplateModelFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenTemplateModelFromRepo == null)
            {
                return NotFound();
            }

            var ScreenTemplateToPatch = _mapper.Map<ScreenTemplateUpdateDto>(ScreenTemplateModelFromRepo);

            patchDoc.ApplyTo(ScreenTemplateToPatch, ModelState);

            if (!TryValidateModel(ScreenTemplateToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(ScreenTemplateToPatch, ScreenTemplateModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(ScreenTemplateModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
