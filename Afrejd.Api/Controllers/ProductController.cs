using Afrejd.Web.Data;
using Afrejd.Web.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afrejd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext Context;

        public ProductController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product newProduct)
        {
            try
            {
                Context.Products.Add(newProduct);
                await Context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
            }
            catch (DbUpdateException)
            {
                // Handle database update exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Kunde inte spara produkten, var vänlig och försök igen.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
