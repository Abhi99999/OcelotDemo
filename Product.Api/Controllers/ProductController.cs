using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1500 },
            new Product { Id = 2, Name = "Smartphone", Price = 800 },
            new Product { Id = 3, Name = "Tablet", Price = 500 }
        };
        
        // GET: api/product
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Products);
        }

        // GET: api/product/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            // For testing only: just return the product with a fake ID
            product.Id = Products.Max(p => p.Id) + 1;
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            // For testing only: just check if it exists
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            // Do not remove from list (predefined list is readonly for test)
            return NoContent();
        }
    }
}

