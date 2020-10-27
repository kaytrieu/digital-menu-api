using AutoMapper;
using DigitalMenuApi.Dtos.BoxTypeDtos;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
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
        public IActionResult GetBoxType(int page = 1, int limit = 10, string searchValue = "")
        {
            PagingDto<BoxType> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true && x.Name.Contains(searchValue));

            var boxTypes = _mapper.Map<IEnumerable<BoxTypeReadDto>>(dto.Result);

            var response = new PagingResponseDto<BoxTypeReadDto> { Result = boxTypes, Count = dto.Count };
            if (limit > 0)
            {
                if (dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit, searchValue });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit, searchValue });
            }

            return Ok(response);
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
            BoxType BoxTypeModelFromRepo = _repository.Get(x => x.Id == id);

            if (BoxTypeModelFromRepo == null)
            {
                return NotFound();
            }

            BoxTypeUpdateDto BoxTypeToPatch = _mapper.Map<BoxTypeUpdateDto>(BoxTypeModelFromRepo);

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
