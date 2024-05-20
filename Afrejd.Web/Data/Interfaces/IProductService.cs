using Afrejd.Web.Data.Models;

namespace Afrejd.Web.Data.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int productId);
        Task DeleteProduct(int productId);
        Task UpdateProduct(Product product);
    }
}
