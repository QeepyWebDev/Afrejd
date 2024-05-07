using Afrejd.Web.Data.Models;

namespace Afrejd.Web.Data.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task ChangeOrderStatus(int orderId, Order.OrderStatus newStatus);
        Task DeleteOrder(int orderId);
        Task<List<ApplicationUser>> GetUsers();
        Task PlaceOrder(CustomerInfo customerInfo, string userId, List<Cart> CartItems);
        Task ConfirmOrder(int orderId);
        Task<List<Order>> GetConfirmedOrders();
        Task UpdatePriceEstimate(int orderId, decimal? newPriceEstimate);
    }
}
