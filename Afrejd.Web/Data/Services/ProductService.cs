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

        public async Task<Product> GetProduct(int productId)
        {
            return await applicationDbContext.Products
                .FirstOrDefaultAsync(p => p.Id == productId);
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

        public async Task UpdateProduct(Product product)
        {
            if (product.Id != 0)
            {
                var existingProduct = await applicationDbContext.Products.FindAsync(product.Id);
                if (existingProduct != null)
                {
                    applicationDbContext.Entry(existingProduct).State = EntityState.Detached;
                }

                var updatedProduct = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId
                };

                applicationDbContext.Products.Update(updatedProduct);
                await applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Invalid product ID: {product.Id}");
            }
        }
    }
}
