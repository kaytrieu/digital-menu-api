using AutoMapper;
using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
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

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProduct()
        {
            IEnumerable<Product> Products = _repository.GetAll(x => x.Store);
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(Products));
            //return Ok(Products);
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductReadDto> GetProduct(int id)
        {
            Product Product = _repository.Get(x => x.Id == id, x => x.Store);

            if (Product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductReadDto>(Product));
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
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
        public IActionResult PostProduct(ProductCreateDto ProductCreateDto)
        {
            Product ProductModel = _mapper.Map<Product>(ProductCreateDto);

            _repository.Add(ProductModel);
            _repository.SaveChanges();

            ProductReadDto ProductReadDto = _mapper.Map<ProductReadDto>(ProductModel);

            return CreatedAtAction("GetProduct", new { id = ProductReadDto.Id }, ProductCreateDto);

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
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
        public IActionResult PatchProduct(int id, JsonPatchDocument<ProductUpdateDto> patchDoc)
        {
            var ProductModelFromRepo = _repository.Get(x => x.Id == id);

            if (ProductModelFromRepo == null)
            {
                return NotFound();
            }

            var ProductToPatch = _mapper.Map<ProductUpdateDto>(ProductModelFromRepo);

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
