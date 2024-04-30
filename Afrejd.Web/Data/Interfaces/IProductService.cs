using Afrejd.Web.Data.Models;

namespace Afrejd.Web.Data.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<List<Product>> GetProducts();
        Task DeleteProduct(int productId);
    }
}
