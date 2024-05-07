using Afrejd.Web.Data.Interfaces;
using Afrejd.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Afrejd.Web.Data.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext Context;

        public UserService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<List<ApplicationUser>> GetCustomers()
        {
            return await Context.Users
                .Where(u => u.Orders.Any())
                .Include(u => u.CustomerInfo)
                .Include(u => u.Orders)
                .ToListAsync();
        }
    }
}
