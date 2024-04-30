using Afrejd.Web.Data.Interfaces;
using Afrejd.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Afrejd.Web.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddProduct(Product product)
        {
            applicationDbContext.Products.Add(product);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await applicationDbContext.Products.ToListAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await applicationDbContext.Products.FindAsync(productId);
            if (product != null)
            {
                applicationDbContext.Products.Remove(product);
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
