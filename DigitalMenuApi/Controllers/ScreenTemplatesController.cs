using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ScreenTemplateDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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

        //store, staff get inside, SA get all
        // GET: api/ScreenTemplates
        [Authorize]
        [HttpGet]
        public ActionResult<PagingResponseDto<ScreenTemplateReadDto>> GetScreenTemplate(int page = 1, int limit = 10)
        {
            PagingDto<ScreenTemplate> dto;

            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;

            var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;


            if (role == Role.Admin || role == Role.Staff)
            {
                var storeId = int.Parse(claims.Where(x => x.Type == "storeId").FirstOrDefault().Value);
                dto = _repository.GetAll(page, limit, x => x.IsAvailable == true && x.Screen.StoreId == storeId, x => x.Screen);
            }

            //if (role == Role.Staff)
            //{
            //    var accountId = claims.Where(x => x.Type == "accountId").FirstOrDefault().Value;

            //    if (account.Id != int.Parse(accountId))
            //    {
            //        return Forbid("You can only get your account");
            //    }
            //}
            else
            {
                dto = _repository.GetAll(page, limit, x => x.IsAvailable == true);
            }

            var screenTemplate = _mapper.Map<IEnumerable<ScreenTemplateReadDto>>(dto.Result);

            var response = new PagingResponseDto<ScreenTemplateReadDto> { Result = screenTemplate, Count = dto.Count };
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

        //store, staff get inside, SA get all

        // GET: api/ScreenTemplates/5
        [Authorize]
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

        //store, staff get inside, SA get all

        // PUT: api/ScreenTemplates/5
        [Authorize]
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

        //store, staff get inside

        // POST: api/ScreenTemplates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public IActionResult PostScreenTemplate(ScreenTemplateCreateDto ScreenTemplateCreateDto)
        {
            ScreenTemplate ScreenTemplateModel = _mapper.Map<ScreenTemplate>(ScreenTemplateCreateDto);

            _repository.Add(ScreenTemplateModel);
            _repository.SaveChanges();

            ScreenTemplateReadDto ScreenTemplateReadDto = _mapper.Map<ScreenTemplateReadDto>(ScreenTemplateModel);

            return CreatedAtAction("GetScreenTemplate", new { id = ScreenTemplateReadDto.Id }, ScreenTemplateReadDto);

        }

        //store, staff get inside, SA get all

        // DELETE: api/ScreenTemplates/5
        [Authorize]
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
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult PatchScreenTemplate(int id, JsonPatchDocument<ScreenTemplateUpdateDto> patchDoc)
        {
            ScreenTemplate ScreenTemplateModelFromRepo = _repository.Get(x => x.Id == id);

            if (ScreenTemplateModelFromRepo == null)
            {
                return NotFound();
            }

            ScreenTemplateUpdateDto ScreenTemplateToPatch = _mapper.Map<ScreenTemplateUpdateDto>(ScreenTemplateModelFromRepo);

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
