using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ScreenDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static DigitalMenuApi.Models.Extensions.Extensions;

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
        //superadmin
        // GET: api/Screens
        [HttpGet]
        [AuthorizeRoles(Role.SuperAdmin)]
        public ActionResult<PagingResponseDto<ScreenReadDto>> GetScreen(int page = 1, int limit = 10)
        {
            PagingDto<Screen> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true, x=> x.Store);

            var screen = _mapper.Map<IEnumerable<ScreenReadDto>>(dto.Result);

            var response = new PagingResponseDto<ScreenReadDto> { Result = screen, Count = dto.Count };
            if (limit > 0)
            {
                if ((double)dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit });
            }

            return Ok(response);
        }

        //store get instore, staff get instore 
        // GET: api/Screens/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ScreenReadDto> GetScreen(int id)
        {
            Screen Screen = _repository.Get(x => x.Id == id && x.IsAvailable == true, x=> x.Store);

            if (Screen == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScreenReadDto>(Screen));
        }

        [HttpGet("udid/{udid}")]
        [Authorize]
        public ActionResult<ScreenReadDto> GetScreenByUdid(string udid)
        {
            Screen Screen = _repository.Get(x => x.Uid == udid && x.IsAvailable == true);

            if (Screen == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScreenReadDto>(Screen));
        }

        // admin
        // PUT: api/Screens/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PutScreen(int id, ScreenUpdateDto ScreenUpdateDto)
        {
            Screen ScreenFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenFromRepo == null)
            {
                return NotFound();
            }


            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            if (role == Role.Admin || role == Role.Staff)
            {
                var storeId = claims.Where(x => x.Type == "storeId").FirstOrDefault().Value;

                if (!(ScreenFromRepo.StoreId == int.Parse(storeId)))
                {
                    return Forbid("You can only edit a store's screen");
                }
            }

            //Mapper to Update
            _mapper.Map(ScreenUpdateDto, ScreenFromRepo);

            _repository.Update(ScreenFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // //store
        // POST: api/Screens
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public IActionResult PostScreen(ScreenCreateDto ScreenCreateDto)
        {
            Screen ScreenModel = _mapper.Map<Screen>(ScreenCreateDto);


            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            if (role == Role.Admin || role == Role.Staff)
            {
                var storeId = claims.Where(x => x.Type == "storeId").FirstOrDefault().Value;

                if (!(ScreenModel.StoreId == int.Parse(storeId)))
                {
                    return Forbid("You can only create a store's screen");
                }
            }

            _repository.Add(ScreenModel);
            _repository.SaveChanges();

            ScreenReadDto ScreenReadDto = _mapper.Map<ScreenReadDto>(ScreenModel);

            return CreatedAtAction("GetScreen", new { id = ScreenReadDto.Id }, ScreenReadDto);

        }

        //store, SA
        // DELETE: api/Screens/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteScreen(int id)
        {
            Screen ScreenFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenFromRepo == null)
            {
                return NotFound();
            }

            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            if (role == Role.Admin || role == Role.Staff)
            {
                var storeId = claims.Where(x => x.Type == "storeId").FirstOrDefault().Value;

                if (!(ScreenFromRepo.StoreId == int.Parse(storeId)))
                {
                    return Forbid("You can only delete your store's screen");
                }
            }

            _repository.Delete(ScreenFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult PatchScreen(int id, JsonPatchDocument<ScreenUpdateDto> patchDoc)
        {
            Screen ScreenModelFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenModelFromRepo == null)
            {
                return NotFound();
            }

            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

            if (role == Role.Admin || role == Role.Staff)
            {
                var storeId = claims.Where(x => x.Type == "storeId").FirstOrDefault().Value;

                if (!(ScreenModelFromRepo.StoreId == int.Parse(storeId)))
                {
                    return Forbid("You can only edit a store's screen");
                }
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
