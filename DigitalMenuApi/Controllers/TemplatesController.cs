using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.TemplateDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using DigitalMenuApi.Models.Extensions;
using DigitalMenuApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static DigitalMenuApi.Models.Extensions.Extensions;

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
        public ActionResult<PagingResponseDto<TemplateReadDto>> GetTemplate(int page = 1, int limit = 10, string tag = "", string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();
            tag = tag.IsNullOrEmpty() ? "" : tag.Trim();

            PagingDto<Template> dto = _templateRepository.GetAll(page, limit, predicate: x => x.IsAvailable == true
                                                                      && (x.Tags.ToLower().Contains(tag.ToLower())
                                                                      || x.Name.ToLower().Contains(searchValue.ToLower())));

            IEnumerable<TemplateReadDto> templates = _mapper.Map<IEnumerable<TemplateReadDto>>(dto.Result);

            var response = new PagingResponseDto<TemplateReadDto> { Result = templates, Count = dto.Count };

            if (limit > 0)
            {
                if ((double)dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit, tag, searchValue });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit, tag, searchValue });
            }
            return Ok(response);
        }

        [HttpGet("sample")]
        public ActionResult<PagingResponseDto<TemplateReadDto>> GetSampleTemplate(int page = 1, int limit = 10, string tag = "", string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();
            tag = tag.IsNullOrEmpty() ? "" : tag.Trim();

            PagingDto<Template> dto = _templateRepository.GetAll(page, limit, predicate: x => x.IsAvailable == true
                                                                      && (x.Tags.ToLower().Contains(tag.ToLower())
                                                                            || x.Name.ToLower().Contains(searchValue.ToLower()))
                                                                      && (x.StoreId == null));

            IEnumerable<TemplateReadDto> templates = _mapper.Map<IEnumerable<TemplateReadDto>>(dto.Result);

            var response = new PagingResponseDto<TemplateReadDto> { Result = templates, Count = dto.Count };

            if (limit > 0)
            {
                if ((double)dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit, tag, searchValue });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit, tag, searchValue });
            }
            return Ok(response);
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

        [HttpGet("udid/{udid}")]
        [AuthorizeRoles(Roles =Role.Admin)]
        public ActionResult<TemplateDetailReadDto> GetDetailTemplatebyUDID(string udid)
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            List<Claim> claim = identify.Claims.ToList();
            if(claim.Count < 3 || !claim[2].Type.Equals("storeId"))
            {
                return Forbid();
            }

            int storeId = int.Parse(claim[2].Value);

            int templateId = _templateService.GetTemplateIdFromUDID(udid, storeId);

            Template templateFromRepo = _templateRepository.Get(x => x.Id == templateId && x.IsAvailable == true,
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
            if (_templateService.UpdateTemplateDetail(id, TemplateUpdateDto))
            {
                return NoContent();
            }

            return NotFound();
        }

        // POST: api/Templates
        [HttpPost]
        public IActionResult PostTemplate([FromForm] TemplatePostFormWrapper formWrapper)
        {
            var file = formWrapper.file;
            TemplateCreateDto templateDto = JsonConvert.DeserializeObject<TemplateCreateDto>(formWrapper.templateDtoJson);
            if (file.Length > 0)
            {
                string uploadedFileLink = FirebaseService.UploadFileToFirebaseStorage(file.OpenReadStream(), DateTime.Now.ToString("ddMMyyyyHHmmssff") + file.FileName, "UiLinkJsFile").Result;

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

        [HttpPost("/{sample-template-id}/StoreTemplate")]
        public IActionResult CloneTemplateForStore([FromRoute(Name = "sample-template-id")] int sampleTemplateId, TemplateCreateDto templateDto)
        {
            string uiLink = _templateRepository.Get(x => x.Id == sampleTemplateId).Uilink;

            Template createdTemplate = _templateService.CreateNewTemplate(templateDto, uiLink);

            TemplateDetailReadDto TemplateReadDto = _mapper.Map<TemplateDetailReadDto>(createdTemplate);

            return CreatedAtAction("GetTemplate", new { id = createdTemplate.Id }, TemplateReadDto);
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
