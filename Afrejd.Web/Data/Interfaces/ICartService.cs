using Afrejd.Web.Data.Models;

namespace Afrejd.Web.Data.Interfaces
{
    public interface ICartService
    {
        Task AddItemToCart(int productId, string userId);
        Task<List<Cart>> GetUserCartItems(string userId);
        Task RemoveItemFromCart(int productId, string userId);
        Task<string> GetUserId();
    }
}
