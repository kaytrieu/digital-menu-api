using AutoMapper;
using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Dtos.StoreDtos;
using DigitalMenuApi.Models;
using DigitalMenuApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public StoresController(IStoreRepository repository, IMapper mapper, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        // GET: api/Stores
        [HttpGet]
        public IActionResult GetStore()
        {
            IEnumerable<Store> Stores = _repository.GetAll(x => x.IsAvailable == true);
            return Ok(_mapper.Map<IEnumerable<StoreReadDto>>(Stores));
            //return Ok(Stores);
        }


        // GET: api/Stores/5
        [HttpGet("{id}")]
        public ActionResult<StoreReadDto> GetStore(int id)
        {
            Store Store = _repository.Get(x => x.Id == id && x.IsAvailable == true);

            if (Store == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StoreReadDto>(Store));
        }

        // GET: api/Stores/5/Product
        [HttpGet("{id}/Products")]
        public IActionResult GetAllProductOfStore(int id)
        {
            IEnumerable<Product> Products = _productRepository.GetAll(
                predicate: x => x.IsAvailable == true && x.StoreId == id, 
                including: x => x.Store);
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(Products));
        }

        // PUT: api/Stores/5
        [HttpPut("{id}")]
        public IActionResult PutStore(int id, StoreUpdateDto StoreUpdateDto)
        {
            Store StoreFromRepo = _repository.Get(x => x.Id == id);

            if (StoreFromRepo == null)
            {
                return NotFound();
            }

            //Mapper to Update
            _mapper.Map(StoreUpdateDto, StoreFromRepo);

            _repository.Update(StoreFromRepo);

            _repository.SaveChanges();


            return NoContent();
        }

        // POST: api/Stores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostStore(StoreCreateDto StoreCreateDto)
        {
            Store StoreModel = _mapper.Map<Store>(StoreCreateDto);

            _repository.Add(StoreModel);
            _repository.SaveChanges();

            StoreReadDto StoreReadDto = _mapper.Map<StoreReadDto>(StoreModel);

            return CreatedAtAction("GetStore", new { id = StoreReadDto.Id }, StoreCreateDto);

        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStore(int id)
        {
            Store StoreFromRepo = _repository.Get(x => x.Id == id);

            if (StoreFromRepo == null)
            {
                return NotFound();
            }

            _repository.Delete(StoreFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //Patch
        [HttpPatch("{id}")]
        public IActionResult PatchStore(int id, JsonPatchDocument<StoreUpdateDto> patchDoc)
        {
            var StoreModelFromRepo = _repository.Get(x => x.Id == id);

            if (StoreModelFromRepo == null)
            {
                return NotFound();
            }

            var StoreToPatch = _mapper.Map<StoreUpdateDto>(StoreModelFromRepo);

            patchDoc.ApplyTo(StoreToPatch, ModelState);

            if (!TryValidateModel(StoreToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //Update the DTO to repo
            _mapper.Map(StoreToPatch, StoreModelFromRepo);

            //Temp is not doing nothing
            _repository.Update(StoreModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
