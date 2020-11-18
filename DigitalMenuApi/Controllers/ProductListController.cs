using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static DigitalMenuApi.Models.Extensions.Extensions;

namespace DigitalMenuApi.Controllers
{
    //superadmin
    [Route("api/product-lists")]
    [ApiController]
    public class ProductListsController : ControllerBase
    {
        private readonly IProductListRepository _repository;
        private readonly IMapper _mapper;

        public ProductListsController(IProductListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ProductLists
        [HttpGet]
        [AuthorizeRoles(Role.SuperAdmin)]
        public ActionResult<PagingResponseDto<ProductListReadDto>> GetProductList(int page = 1, int limit = 10)
        {
            PagingDto<ProductList> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true);

            var productList = _mapper.Map<IEnumerable<ProductListReadDto>>(dto.Result);

            var response = new PagingResponseDto<ProductListReadDto> { Result = productList, Count = dto.Count };
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


        // GET: api/ProductLists/5
        [HttpGet("{id}")]
        [AuthorizeRoles(Role.SuperAdmin)]
        public ActionResult<ProductListReadDto> GetProductList(int id)
        {
            ProductList ProductList = _repository.Get(predicate: x => x.Id == id && x.IsAvailable == true);

            if (ProductList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductListReadDto>(ProductList));
        }

        // PUT: api/ProductLists/5
        [HttpPut("{id}")]
        [AuthorizeRoles(Role.SuperAdmin)]
        public IActionResult PutProductList(int id, ProductListUpdateDto ProductListUpdateDto)
        {
            ProductList ProductListFromRepo = _repository.Get(x => x.Id == id);

            if (ProductListFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(ProductListUpdateDto, ProductListFromRepo);

            _repository.Update(ProductListFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/ProductLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AuthorizeRoles(Role.SuperAdmin)]
        public IActionResult PostProductList(ProductListCreateDto ProductListCreateDto)
        {
            ProductList ProductListModel = _mapper.Map<ProductList>(ProductListCreateDto);

            _repository.Add(ProductListModel);
            _repository.SaveChanges();

            ProductListReadDto ProductListReadDto = _mapper.Map<ProductListReadDto>(ProductListModel);

            return CreatedAtAction("GetProductList", new { id = ProductListReadDto.Id }, ProductListReadDto);

        }

        // DELETE: api/ProductLists/5
        [AuthorizeRoles(Role.SuperAdmin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteProductList(int id)
        {
            ProductList ProductListFromRepo = _repository.Get(x => x.Id == id);

            if (ProductListFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(ProductListFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [AuthorizeRoles(Role.SuperAdmin)]
        [HttpPatch("{id}")]
        public IActionResult PatchProductList(int id, JsonPatchDocument<ProductListUpdateDto> patchDoc)
        {
            ProductList ProductListModelFromRepo = _repository.Get(x => x.Id == id);

            if (ProductListModelFromRepo == null)
            {
                return NotFound();
            }

            ProductListUpdateDto ProductListToPatch = _mapper.Map<ProductListUpdateDto>(ProductListModelFromRepo);

            patchDoc.ApplyTo(ProductListToPatch, ModelState);

            if (!TryValidateModel(ProductListToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(ProductListToPatch, ProductListModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(ProductListModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
