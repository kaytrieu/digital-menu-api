using AutoMapper;
using DigitalMenuApi.Constant;
using DigitalMenuApi.Dtos.AccountDtos;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Dtos.ProductDtos;
using DigitalMenuApi.Dtos.ScreenDtos;
using DigitalMenuApi.Dtos.StoreDtos;
using DigitalMenuApi.Dtos.TemplateDtos;
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
    //SA
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ITemplateRepository _templateRepository;
        private readonly IScreenRepository _screenRepository;

        public StoresController(IStoreRepository repository, IMapper mapper, IProductRepository productRepository, IAccountRepository accountRepository, ITemplateRepository templateRepository, IScreenRepository screenRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _templateRepository = templateRepository;
            _screenRepository = screenRepository;
        }

        // GET: api/Stores
        [HttpGet]
        [AuthorizeRoles(Role.SuperAdmin]
        public ActionResult<PagingResponseDto<StoreReadDto>> GetStore(int page = 1, int limit = 10, string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

            PagingDto<Store> dto = _repository.GetAll(page, limit, x => x.IsAvailable == true && x.Name.Contains(searchValue));

            var store = _mapper.Map<IEnumerable<StoreReadDto>>(dto.Result);

            var response = new PagingResponseDto<StoreReadDto> { Result = store, Count = dto.Count };
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
        }


        // GET: api/Stores/5
        [Authorize]
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

        // all
        // GET: api/Stores/5/Product
        [Authorize]
        [HttpGet("{id}/Products")]
        public ActionResult<PagingResponseDto<ProductReadDto>> GetAllProductOfStore(int id, int page = 1, int limit = 10, string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

            PagingDto<Product> dto = _productRepository.GetAll(page, limit, predicate: x => x.IsAvailable == true && x.StoreId == id && x.Title.Contains(searchValue), x => x.Store);

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

        //store
        //Put: api/products/batch-price
        [Authorize]
        [HttpPut("{storeId}/Products/batch-price")]
        public IActionResult ChangePriceByBatch(int storeId, List<ProductUpdatePriceDto> productDtos)
        {
            List<int> notFoundIds = new List<int>();

            foreach (var productDto in productDtos)
            {

                Product ProductFromRepo = _productRepository.Get(x => x.Id == productDto.Id && x.StoreId == storeId);

                if (ProductFromRepo == null)
                {
                    notFoundIds.Add(productDto.Id);

                }
                else
                {
                    _mapper.Map(productDto, ProductFromRepo);

                    _productRepository.SaveChanges();
                }
            }

            if (notFoundIds.Count > 0)
            {
                return UnprocessableEntity("Product id: [" + string.Join(",", notFoundIds) + "] are not found in store.");
            }

            return NoContent();
        }

        //store
        [Authorize]
        [HttpGet("{id}/Accounts")]
        public ActionResult<PagingResponseDto<AccountReadDto>> GetAllAccountOfStore(int id, int page= 1, int limit = 10, string searchValue = "")
        {
            searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

            PagingDto<Account> dto = _accountRepository.GetAll(page, limit, x => x.IsAvailable == true && x.StoreId == id && x.Username.Contains(searchValue), x => x.Role, x => x.Store);

            var accounts = _mapper.Map<IEnumerable<AccountReadDto>>(dto.Result);

            var response = new PagingResponseDto<AccountReadDto> { Result = accounts, Count = dto.Count };

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
        }

        //store, staff
        [Authorize]
        [HttpGet("{id}/Templates")]
        public ActionResult<PagingResponseDto<TemplateReadDto>> GetAllTemplateOfStore(int id, int page = 1, int limit = 10, string tag = "", string searchValue = "")
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

        //store staff
        [Authorize]
        [HttpGet("{id}/Screens")]
        public ActionResult<PagingResponseDto<ScreenReadDto>> GetAllScreenOfStore(int id, int page = 1, int limit = 10)
        {
            PagingDto<Screen> dto = _screenRepository.GetAll(page, limit, x => x.IsAvailable == true && x.StoreId == id);

            var screens = _mapper.Map<IEnumerable<ScreenReadDto>>(dto.Result);

            var response = new PagingResponseDto<ScreenReadDto> { Result = screens, Count = dto.Count };

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

        //sa
        // PUT: api/Stores/5
        [Authorize]
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

        //sa
        // POST: api/Stores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public IActionResult PostStore(StoreCreateDto StoreCreateDto)
        {
            Store StoreModel = _mapper.Map<Store>(StoreCreateDto);

            _repository.Add(StoreModel);
            _repository.SaveChanges();

            StoreReadDto StoreReadDto = _mapper.Map<StoreReadDto>(StoreModel);

            return CreatedAtAction("GetStore", new { id = StoreReadDto.Id }, StoreCreateDto);

        }

        //sa
        // DELETE: api/Stores/5
        [Authorize]
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
        [Authorize]
        [HttpPatch("{id}")]
        public IActionResult PatchStore(int id, JsonPatchDocument<StoreUpdateDto> patchDoc)
        {
            Store StoreModelFromRepo = _repository.Get(x => x.Id == id);

            if (StoreModelFromRepo == null)
            {
                return NotFound();
            }

            StoreUpdateDto StoreToPatch = _mapper.Map<StoreUpdateDto>(StoreModelFromRepo);

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
