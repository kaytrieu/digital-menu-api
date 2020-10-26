using AutoMapper;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ScreenDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenRepository _repository;
        private readonly IMapper _mapper;

        public ScreensController(IScreenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Screens
        [HttpGet]
        public IActionResult GetScreen(int page = 1, int limit = 10)
        {
            PagingDto<Screen> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true);

            var screen = _mapper.Map<IEnumerable<ScreenReadDto>>(dto.Result);

            var response = new PagingResponseDto<ScreenReadDto> { Result = screen, Count = dto.Count };
            if (limit > 0)
            {
                if (dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit });
            }

            return Ok(response);
        }


        // GET: api/Screens/5
        [HttpGet("{id}")]
        public ActionResult<ScreenReadDto> GetScreen(int id)
        {
            Screen Screen = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (Screen == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScreenReadDto>(Screen));
        }

        // PUT: api/Screens/5
        [HttpPut("{id}")]
        public IActionResult PutScreen(int id, ScreenUpdateDto ScreenUpdateDto)
        {
            Screen ScreenFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(ScreenUpdateDto, ScreenFromRepo);

            _repository.Update(ScreenFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/Screens
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostScreen(ScreenCreateDto ScreenCreateDto)
        {
            Screen ScreenModel = _mapper.Map<Screen>(ScreenCreateDto);

            _repository.Add(ScreenModel);
            _repository.SaveChanges();

            ScreenReadDto ScreenReadDto = _mapper.Map<ScreenReadDto>(ScreenModel);

            return CreatedAtAction("GetScreen", new { id = ScreenReadDto.Id }, ScreenCreateDto);

        }

        // DELETE: api/Screens/5
        [HttpDelete("{id}")]
        public IActionResult DeleteScreen(int id)
        {
            Screen ScreenFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(ScreenFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchScreen(int id, JsonPatchDocument<ScreenUpdateDto> patchDoc)
        {
            Screen ScreenModelFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenModelFromRepo == null)
            {
                return NotFound();
            }

            ScreenUpdateDto ScreenToPatch = _mapper.Map<ScreenUpdateDto>(ScreenModelFromRepo);

            patchDoc.ApplyTo(ScreenToPatch, ModelState);

            if (!TryValidateModel(ScreenToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(ScreenToPatch, ScreenModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(ScreenModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
