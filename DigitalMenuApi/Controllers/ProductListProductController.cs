using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListProductController : ControllerBase
    {
        private readonly DigitalMenuBoxContext _context;

        public ProductListProductController(DigitalMenuBoxContext context)
        {
            _context = context;
        }

        // GET: api/ProductListProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductListProduct>>> GetProductListProduct()
        {
            return await _context.ProductListProduct.ToListAsync();
        }

        // GET: api/ProductListProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductListProduct>> GetProductListProduct(int id)
        {
            var productListProduct = await _context.ProductListProduct.FindAsync(id);

            if (productListProduct == null)
            {
                return NotFound();
            }

            return productListProduct;
        }

        // PUT: api/ProductListProduct/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductListProduct(int id, ProductListProduct productListProduct)
        {
            if (id != productListProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(productListProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductListProductExists(id))
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

        // POST: api/ProductListProduct
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductListProduct>> PostProductListProduct(ProductListProduct productListProduct)
        {
            _context.ProductListProduct.Add(productListProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductListProductExists(productListProduct.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductListProduct", new { id = productListProduct.Id }, productListProduct);
        }

        // DELETE: api/ProductListProduct/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductListProduct>> DeleteProductListProduct(int id)
        {
            var productListProduct = await _context.ProductListProduct.FindAsync(id);
            if (productListProduct == null)
            {
                return NotFound();
            }

            _context.ProductListProduct.Remove(productListProduct);
            await _context.SaveChangesAsync();

            return productListProduct;
        }

        private bool ProductListProductExists(int id)
        {
            return _context.ProductListProduct.Any(e => e.Id == id);
        }
    }
}
