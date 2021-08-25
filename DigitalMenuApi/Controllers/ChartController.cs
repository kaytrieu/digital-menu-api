using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMenuApi.GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ChartController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/Products
        //[HttpGet]
        //public ActionResult<List<ChartProductCountInStoreDto>> GetProductProductCountInStore(int page = 1, int limit = 10, string searchValue = "")
        //{
        //    searchValue = searchValue.IsNullOrEmpty() ? "" : searchValue.Trim();

        //    PagingDto<Product> dto = _repository.GetAll(page, limit, predicate: x => x.IsAvailable == true && x.Title.Contains(searchValue), x => x.Store);

        //    var product = _mapper.Map<IEnumerable<ProductReadDto>>(dto.Result);

        //    var response = new PagingResponseDto<ProductReadDto> { Result = product, Count = dto.Count };
        //    if (limit > 0)
        //    {
        //        if ((double)dto.Count / limit > page)
        //        {
        //            response.NextPage = Url.Link(null, new { page = page + 1, limit, searchValue });
        //        }

        //        if (page > 1)
        //            response.PreviousPage = Url.Link(null, new { page = page - 1, limit, searchValue });
        //    }

        //    return Ok(response);
        //    //return Ok(Products);
        //}

    }
}
