using Afrejd.Web.Data.Models;

namespace Afrejd.Web.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetCustomers();
        Task AddOrChangeUserInfo(CustomerInfo customerInfo, string userId);
        Task<CustomerInfo> GetUserInfo(string userId);
        Task<string> GetCurrentUserId();
        Task<List<Order>> GetUserOrders(string userId);
        Task<string> GetUserCompany(string userId);
    }
}
