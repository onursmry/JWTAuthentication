using IsProje.Data;
using IsProje.Models;
using IsProje.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IsProje.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public static List<Product> productList = new List<Product>();

        private readonly ProjectContext _context;
        public ProductController(ProjectContext context)
        {
            _context = context;

        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductModel model)
        {
            Product product = new Product();
            product.Id = model.Id;
            product.ProductName = model.ProductName;
            product.Price = model.Price;
            _context.Products.Add(product);
            productList.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new
            {
                Id = model.Id

            }, model);
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProduct(List<Product> productList)
        {
            if (productList == null)
            {
                return BadRequest(new
                {
                    message = "Product list is empty."
                });
            }
            return Ok(productList);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(ProductModel model)
        {
            var updateProduct = await _context.Products.FindAsync(model.Id);
            if (updateProduct == null)
            {
                return NotFound();
            }
            updateProduct.Id = model.Id;
            updateProduct.ProductName = model.ProductName;
            updateProduct.Price = model.Price;
            _context.Products.Add(updateProduct);
            await _context.SaveChangesAsync();

            return (updateProduct);

        }
    }
}


