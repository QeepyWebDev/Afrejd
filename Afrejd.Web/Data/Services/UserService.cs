using Afrejd.Web.Data.Interfaces;
using Afrejd.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Afrejd.Web.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly UserManager<ApplicationUser> UserManager;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            HttpContextAccessor = httpContextAccessor;
            UserManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetCustomers()
        {
            return await Context.Users
                .Where(u => u.Orders.Any())
                .Include(u => u.CustomerInfo)
                .Include(u => u.Orders)
                .ToListAsync();
        }

        public async Task AddOrChangeUserInfo(CustomerInfo customerInfo, string userId)
        {
            if (customerInfo.Id == 0)
            {
                customerInfo.UserId = userId;
                var existingCustomerInfo = await Context.CustomerInfo
                    .FirstOrDefaultAsync(ci => ci.UserId == userId);

                if (existingCustomerInfo != null)
                {
                    existingCustomerInfo.FirstName = customerInfo.FirstName;
                    existingCustomerInfo.LastName = customerInfo.LastName;
                    existingCustomerInfo.Address = customerInfo.Address;
                    existingCustomerInfo.City = customerInfo.City;
                    existingCustomerInfo.Region = customerInfo.Region;
                    existingCustomerInfo.PostalCode = customerInfo.PostalCode;
                    existingCustomerInfo.Country = customerInfo.Country;
                    existingCustomerInfo.Phone = customerInfo.Phone;
                    existingCustomerInfo.Company = customerInfo.Company;
                    existingCustomerInfo.OrganizationNumber = customerInfo.OrganizationNumber;

                    await Context.SaveChangesAsync();
                }
                else
                {
                    Context.CustomerInfo.Add(customerInfo);
                    await Context.SaveChangesAsync();
                }
            }
        }
        public async Task<CustomerInfo> GetUserInfo(string userId)
        {
            return await Context.CustomerInfo.FirstOrDefaultAsync(ci => ci.UserId == userId);
        }

        public async Task<string> GetCurrentUserId()
        {
            var user = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);
            return user?.Id;
        }

        public async Task<List<Order>> GetUserOrders(string userId)
        {
            return await Context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<string> GetUserCompany(string userId)
        {
            var customerInfo = await Context.CustomerInfo
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (customerInfo != null)
            {
                return customerInfo.Company;
            }
            else
            {
                return null;
            }
        }
    }
}
