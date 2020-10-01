using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public ProductListController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/ProductList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductList>>> GetProductList()
        {
            return await _context.ProductList.ToListAsync();
        }

        // GET: api/ProductList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductList>> GetProductList(int id)
        {
            ProductList productList = await _context.ProductList.FindAsync(id);

            if (productList == null)
            {
                return NotFound();
            }

            return productList;
        }

        // PUT: api/ProductList/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductList(int id, ProductList productList)
        {
            if (id != productList.Id)
            {
                return BadRequest();
            }

            _context.Entry(productList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductList
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductList>> PostProductList(ProductList productList)
        {
            _context.ProductList.Add(productList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductListExists(productList.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductList", new { id = productList.Id }, productList);
        }

        // DELETE: api/ProductList/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductList>> DeleteProductList(int id)
        {
            ProductList productList = await _context.ProductList.FindAsync(id);
            if (productList == null)
            {
                return NotFound();
            }

            _context.ProductList.Remove(productList);
            await _context.SaveChangesAsync();

            return productList;
        }

        private bool ProductListExists(int id)
        {
            return _context.ProductList.Any(e => e.Id == id);
        }
    }
}
