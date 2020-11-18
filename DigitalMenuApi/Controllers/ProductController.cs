using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using DigitalMenuApi.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static DigitalMenuApi.Models.Extensions.Extensions;

namespace DigitalMenuApi.Controllers
{
    //super admin
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //super admin
        // GET: api/Products
        [HttpGet]
        [AuthorizeRoles(Role.SuperAdmin)]
        public ActionResult<PagingResponseDto<ProductReadDto>> GetProduct(int page = 1, int limit = 10, string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

            PagingDto<Product> dto = _repository.GetAll(page, limit, predicate: x => x.IsAvailable == true && x.Title.Contains(searchValue), x => x.Store);

            var product = _mapper.Map<IEnumerable<ProductReadDto>>(dto.Result);

            var response = new PagingResponseDto<ProductReadDto> { Result = product, Count = dto.Count };
            if (limit > 0)
            {
                if ((double)dto.Count / limit > page)
                {
                    response.NextPage = Url.Link(null, new { page = page + 1, limit, searchValue });
                }

                if (page > 1)
                    response.PreviousPage = Url.Link(null, new { page = page - 1, limit, searchValue });
            }

            return Ok(response);
            //return Ok(Products);
        }

        //superadmin
        // GET: api/Products/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ProductReadDto> GetProduct(int id)
        {
            Product Product = _repository.Get(predicate: x => x.Id == id && x.IsAvailable == true,
                                              including: x => x.Store);

            if (Product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductReadDto>(Product));
        }

        //
        // PUT: api/Products/5
        [HttpPut("{id}")]
        [AuthorizeRoles(Role.SuperAdmin, Role.Admin)]
        public IActionResult PutProduct(int id, ProductUpdateDto ProductUpdateDto)
        {
            Product ProductFromRepo = _repository.Get(x => x.Id == id);

            if (ProductFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(ProductUpdateDto, ProductFromRepo);

            _repository.Update(ProductFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AuthorizeRoles(Role.SuperAdmin, Role.Admin)]
        public IActionResult PostProduct(ProductCreateDto ProductCreateDto)
        {
            Product ProductModel = _mapper.Map<Product>(ProductCreateDto);

            _repository.Add(ProductModel);
            _repository.SaveChanges();

            ProductReadDto ProductReadDto = _mapper.Map<ProductReadDto>(ProductModel);

            return CreatedAtAction("GetProduct", new { id = ProductReadDto.Id }, ProductReadDto);

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.SuperAdmin, Role.Admin)]
        public IActionResult DeleteProduct(int id)
        {
            Product ProductFromRepo = _repository.Get(x => x.Id == id);

            if (ProductFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(ProductFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        [AuthorizeRoles(Role.SuperAdmin, Role.Admin)]
        public IActionResult PatchProduct(int id, JsonPatchDocument<ProductUpdateDto> patchDoc)
        {
            Product ProductModelFromRepo = _repository.Get(x => x.Id == id);

            if (ProductModelFromRepo == null)
            {
                return NotFound();
            }

            ProductUpdateDto ProductToPatch = _mapper.Map<ProductUpdateDto>(ProductModelFromRepo);

            patchDoc.ApplyTo(ProductToPatch, ModelState);

            if (!TryValidateModel(ProductToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(ProductToPatch, ProductModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(ProductModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
