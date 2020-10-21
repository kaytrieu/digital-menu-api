using AutoMapper;
using DigitalMenuApi.Dtos.BoxTypeDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/box-types")]
    [ApiController]
    public class BoxTypesController : ControllerBase
    {
        private readonly IBoxTypeRepository _repository;
        private readonly IMapper _mapper;

        public BoxTypesController(IBoxTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/BoxTypes
        [HttpGet]
        public IActionResult GetBoxType(int page, int limit)
        {
            IEnumerable<BoxType> BoxTypes = _repository.GetAll(page, limit, x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<BoxTypeReadDto>>(BoxTypes));
            //return Ok(BoxTypes);
        }


        // GET: api/BoxTypes/5
        [HttpGet("{id}")]
        public ActionResult<BoxTypeReadDto> GetBoxType(int id)
        {
            BoxType BoxType = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (BoxType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BoxTypeReadDto>(BoxType));
        }

        // PUT: api/BoxTypes/5
        [HttpPut("{id}")]
        public IActionResult PutBoxType(int id, BoxTypeUpdateDto BoxTypeUpdateDto)
        {
            BoxType BoxTypeFromRepo = _repository.Get(x => x.Id == id);

            if (BoxTypeFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(BoxTypeUpdateDto, BoxTypeFromRepo);

            _repository.Update(BoxTypeFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/BoxTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostBoxType(BoxTypeCreateDto BoxTypeCreateDto)
        {
            BoxType BoxTypeModel = _mapper.Map<BoxType>(BoxTypeCreateDto);

            _repository.Add(BoxTypeModel);
            _repository.SaveChanges();

            BoxTypeReadDto BoxTypeReadDto = _mapper.Map<BoxTypeReadDto>(BoxTypeModel);

            return CreatedAtAction("GetBoxType", new { id = BoxTypeReadDto.Id }, BoxTypeCreateDto);

        }

        // DELETE: api/BoxTypes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBoxType(int id)
        {
            BoxType BoxTypeFromRepo = _repository.Get(x => x.Id == id);

            if (BoxTypeFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(BoxTypeFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchBoxType(int id, JsonPatchDocument<BoxTypeUpdateDto> patchDoc)
        {
            var BoxTypeModelFromRepo = _repository.Get(x => x.Id == id);

            if (BoxTypeModelFromRepo == null)
            {
                return NotFound();
            }

            var BoxTypeToPatch = _mapper.Map<BoxTypeUpdateDto>(BoxTypeModelFromRepo);

            patchDoc.ApplyTo(BoxTypeToPatch, ModelState);

            if (!TryValidateModel(BoxTypeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(BoxTypeToPatch, BoxTypeModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(BoxTypeModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
