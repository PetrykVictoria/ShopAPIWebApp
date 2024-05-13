using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPIWebApp.Model;
using ShopAPIWebApp.Models;

namespace ShopAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public ProductsController(ShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Продукту з таким id не знайдено" });
            }

            return product;
        }



        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Продукту з таким id не знайдено." });
            }

            if (product.Price <= 0)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Ціна повинна бути більше від 0." });
            }

            if (!_context.Categories.Any(c => c.Id == product.CategoryId))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Категорію з таким id не знайдено." });
            }

            if (product.IsAvailable != true && product.IsAvailable != false)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Наявність повинна бути представлена у форматі true/false" });
            }

            if (_context.Products.Any(p => p.Name == product.Name))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Продукт з такою назвою вже існує" });
            }

            if (product.Name.Any(char.IsDigit))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Назва продукту має складатись лише з букв" });
            }

            if (!decimal.TryParse(product.Price.ToString(), out _))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Ціна повинна бути числом" });
            }


            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // GET: api/Products/Category/5
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Продуктів за цією категорією не знайдено" });
            }

            return products;
        }

        // GET: api/Products/Search?name=ProductName
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string name)
        {
            var products = await _context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Продукту з таким ім'ям не знайдено" });
            }

            return products;
        }

        // GET: api/Products/CategoryNames
        [HttpGet("CategoryNames")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategoryNames([FromQuery] List<int> ids)
        {
            return await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToListAsync();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Поле не повинно бути порожнім." });
            }

            if (product.Price <= 0)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Ціна повинна бути більше від 0." });
            }

            if (!_context.Categories.Any(c => c.Id == product.CategoryId))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Категорію з таким id не знайдено." });
            }

            if (product.IsAvailable != true && product.IsAvailable != false)
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Наявність повинна бути представлена у форматі true/false" });
            }

            if (_context.Products.Any(p => p.Name == product.Name))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Продукт з такою назвою вже існує" });
            }

            if (product.Name.Any(char.IsDigit))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Назва продукту має складатись лише з букв" });
            }

            if (!decimal.TryParse(product.Price.ToString(), out _))
            {
                return BadRequest(new { status = StatusCodes.Status400BadRequest, message = "Ціна повинна бути числом" });
            }


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { status = StatusCodes.Status404NotFound, message = "Продукту з таким id не знайдено" });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
