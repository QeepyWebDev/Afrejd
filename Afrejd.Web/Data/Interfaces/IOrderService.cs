using Afrejd.Web.Data.Models;

namespace Afrejd.Web.Data.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Task ChangeOrderStatus(int orderId, Order.OrderStatus newStatus);
        Task DeleteOrder(int orderId);
        Task<List<ApplicationUser>> GetUsers();
        Task PlaceOrder(CustomerInfo customerInfo, string userId, List<Cart> CartItems);
    }
}
