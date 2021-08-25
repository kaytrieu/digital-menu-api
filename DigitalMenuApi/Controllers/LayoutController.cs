//using AutoMapper;
//using DigitalMenuApi.Dtos.LayoutDtos;
//using DigitalMenuApi.Dtos.PagingDtos;
//using DigitalMenuApi.Models;
//using DigitalMenuApi.Repository;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//namespace DigitalMenuApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LayoutsController : ControllerBase
//    {
//        private readonly ILayoutRepository _repository;
//        private readonly IMapper _mapper;

//        public LayoutsController(ILayoutRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        // GET: api/Layouts
//        [HttpGet]
//        public ActionResult<PagingResponseDto<LayoutReadDto>> GetLayout(int page = 1, int limit = 10)
//        {
//            PagingDto<Layout> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true);

//            var Layouts = _mapper.Map<IEnumerable<LayoutReadDto>>(dto.Result);

//            var response = new PagingResponseDto<LayoutReadDto> { Result = Layouts, Count = dto.Count };
//            if (limit > 0)
//            {
//                if ((double)dto.Count / limit > page)
//                {
//                    response.NextPage = Url.Link(null, new { page = page + 1, limit });
//                }

//                if (page > 1)
//                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit });
//            }

//            return Ok(response);
//        }


//        // GET: api/Layouts/5
//        [HttpGet("{id}")]
//        public ActionResult<LayoutReadDto> GetLayout(int id)
//        {
//            Layout Layout = _repository.Get(x => x.Id == id && x.IsAvailable == true);

//            if (Layout == null)
//            {
//                return NotFound();
//            }

//            return Ok(_mapper.Map<LayoutReadDto>(Layout));
//        }

//        // PUT: api/Layouts/5
//        [HttpPut("{id}")]
//        public IActionResult PutLayout(int id, LayoutUpdateDto LayoutUpdateDto)
//        {
//            Layout LayoutFromRepo = _repository.Get(x => x.Id == id);

//            if (LayoutFromRepo == null)
//            {
//                return NotFound();
//            }

//            //Mapper to Update
//            _mapper.Map(LayoutUpdateDto, LayoutFromRepo);

//            _repository.Update(LayoutFromRepo);

//            _repository.SaveChanges();


//            return NoContent();
//        }

//        // POST: api/Layouts
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for
//        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//        [HttpPost]
//        public IActionResult PostLayout(LayoutCreateDto LayoutCreateDto)
//        {
//            Layout LayoutModel = _mapper.Map<Layout>(LayoutCreateDto);

//            _repository.Add(LayoutModel);
//            _repository.SaveChanges();

//            LayoutReadDto LayoutReadDto = _mapper.Map<LayoutReadDto>(LayoutModel);

//            return CreatedAtAction("GetLayout", new { id = LayoutReadDto.Id }, LayoutCreateDto);

//        }

//        // DELETE: api/Layouts/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteLayout(int id)
//        {
//            Layout LayoutFromRepo = _repository.Get(x => x.Id == id);

//            if (LayoutFromRepo == null)
//            {
//                return NotFound();
//            }

//            _repository.Delete(LayoutFromRepo);

//            _repository.SaveChanges();

//            return NoContent();
//        }

//        //Patch
//        [HttpPatch("{id}")]
//        public IActionResult PatchLayout(int id, JsonPatchDocument<LayoutUpdateDto> patchDoc)
//        {
//            Layout LayoutModelFromRepo = _repository.Get(x => x.Id == id);

//            if (LayoutModelFromRepo == null)
//            {
//                return NotFound();
//            }

//            LayoutUpdateDto LayoutToPatch = _mapper.Map<LayoutUpdateDto>(LayoutModelFromRepo);

//            patchDoc.ApplyTo(LayoutToPatch, ModelState);

//            if (!TryValidateModel(LayoutToPatch))
//            {
//                return ValidationProblem(ModelState);
//            }

//            //Update the DTO to repo
//            _mapper.Map(LayoutToPatch, LayoutModelFromRepo);

//            //Temp is not doing nothing
//            _repository.Update(LayoutModelFromRepo);

//            _repository.SaveChanges();

//            return NoContent();
//        }
//    }
//}
