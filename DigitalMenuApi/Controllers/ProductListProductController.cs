using AutoMapper;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ProductListProductDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/product-lists-products")]
    [ApiController]
    public class ProductListProductsController : ControllerBase
    {
        private readonly IProductListProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductListProductsController(IProductListProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ProductListProducts
        [HttpGet]
        public ActionResult<PagingResponseDto<ProductListProductReadDto>> GetProductListProduct(int page = 1, int limit = 10)
        {
            PagingDto<ProductListProduct> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true);

            var productListProduct = _mapper.Map<IEnumerable<ProductListProductReadDto>>(dto.Result);

            var response = new PagingResponseDto<ProductListProductReadDto> { Result = productListProduct, Count = dto.Count };
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


        // GET: api/ProductListProducts/5
        [HttpGet("{id}")]
        public ActionResult<ProductListProductReadDto> GetProductListProduct(int id)
        {
            ProductListProduct ProductListProduct = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (ProductListProduct == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductListProductReadDto>(ProductListProduct));
        }

        // PUT: api/ProductListProducts/5
        [HttpPut("{id}")]
        public IActionResult PutProductListProduct(int id, ProductListProductUpdateDto ProductListProductUpdateDto)
        {
            ProductListProduct ProductListProductFromRepo = _repository.Get(x => x.Id == id);

            if (ProductListProductFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(ProductListProductUpdateDto, ProductListProductFromRepo);

            _repository.Update(ProductListProductFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/ProductListProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostProductListProduct(ProductListProductCreateDto ProductListProductCreateDto)
        {
            ProductListProduct ProductListProductModel = _mapper.Map<ProductListProduct>(ProductListProductCreateDto);

            _repository.Add(ProductListProductModel);
            _repository.SaveChanges();

            ProductListProductReadDto ProductListProductReadDto = _mapper.Map<ProductListProductReadDto>(ProductListProductModel);

            return CreatedAtAction("GetProductListProduct", new { id = ProductListProductReadDto.Id }, ProductListProductCreateDto);

        }

        // DELETE: api/ProductListProducts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProductListProduct(int id)
        {
            ProductListProduct ProductListProductFromRepo = _repository.Get(x => x.Id == id);

            if (ProductListProductFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(ProductListProductFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchProductListProduct(int id, JsonPatchDocument<ProductListProductUpdateDto> patchDoc)
        {
            ProductListProduct ProductListProductModelFromRepo = _repository.Get(x => x.Id == id);

            if (ProductListProductModelFromRepo == null)
            {
                return NotFound();
            }

            ProductListProductUpdateDto ProductListProductToPatch = _mapper.Map<ProductListProductUpdateDto>(ProductListProductModelFromRepo);

            patchDoc.ApplyTo(ProductListProductToPatch, ModelState);

            if (!TryValidateModel(ProductListProductToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(ProductListProductToPatch, ProductListProductModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(ProductListProductModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
