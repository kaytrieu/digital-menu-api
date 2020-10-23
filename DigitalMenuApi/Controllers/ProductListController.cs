using AutoMapper;
using DigitalMenuApi.Dtos.ProductListDtos;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
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
        public IActionResult GetProductList(int page, int limit)
        {
            IEnumerable<ProductList> ProductLists = _repository.GetAll(page, limit, x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<ProductListReadDto>>(ProductLists));
            //return Ok(ProductLists);
        }


        // GET: api/ProductLists/5
        [HttpGet("{id}")]
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
        public IActionResult PostProductList(ProductListCreateDto ProductListCreateDto)
        {
            ProductList ProductListModel = _mapper.Map<ProductList>(ProductListCreateDto);

            _repository.Add(ProductListModel);
            _repository.SaveChanges();

            ProductListReadDto ProductListReadDto = _mapper.Map<ProductListReadDto>(ProductListModel);

            return CreatedAtAction("GetProductList", new { id = ProductListReadDto.Id }, ProductListCreateDto);

        }

        // DELETE: api/ProductLists/5
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
