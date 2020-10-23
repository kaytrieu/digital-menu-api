using AutoMapper;
using DigitalMenuApi.Dtos.BoxDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxesController : ControllerBase
    {
        private readonly IBoxRepository _repository;
        private readonly IMapper _mapper;

        public BoxesController(IBoxRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Boxs
        [HttpGet]
        public IActionResult GetBox(int page, int limit)
        {
            IEnumerable<Box> Boxs = _repository.GetAll(page, limit, x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<BoxReadDto>>(Boxs));
            //return Ok(Boxs);
        }


        // GET: api/Boxs/5
        [HttpGet("{id}")]
        public ActionResult<BoxReadDto> GetBox(int id)
        {
            Box Box = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (Box == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BoxReadDto>(Box));
        }

        // PUT: api/Boxs/5
        [HttpPut("{id}")]
        public IActionResult PutBox(int id, BoxUpdateDto BoxUpdateDto)
        {
            Box BoxFromRepo = _repository.Get(x => x.Id == id);

            if (BoxFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(BoxUpdateDto, BoxFromRepo);

            _repository.Update(BoxFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/Boxs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostBox(BoxCreateDto BoxCreateDto)
        {
            Box BoxModel = _mapper.Map<Box>(BoxCreateDto);

            _repository.Add(BoxModel);
            _repository.SaveChanges();

            BoxReadDto BoxReadDto = _mapper.Map<BoxReadDto>(BoxModel);

            return CreatedAtAction("GetBox", new { id = BoxReadDto.Id }, BoxCreateDto);

        }

        // DELETE: api/Boxs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBox(int id)
        {
            Box BoxFromRepo = _repository.Get(x => x.Id == id);

            if (BoxFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(BoxFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchBox(int id, JsonPatchDocument<BoxUpdateDto> patchDoc)
        {
            Box BoxModelFromRepo = _repository.Get(x => x.Id == id);

            if (BoxModelFromRepo == null)
            {
                return NotFound();
            }

            BoxUpdateDto BoxToPatch = _mapper.Map<BoxUpdateDto>(BoxModelFromRepo);

            patchDoc.ApplyTo(BoxToPatch, ModelState);

            if (!TryValidateModel(BoxToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(BoxToPatch, BoxModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(BoxModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
