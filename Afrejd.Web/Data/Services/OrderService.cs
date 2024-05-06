using Afrejd.Web.Data.Interfaces;
using Afrejd.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Afrejd.Web.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext Context;

        public OrderService(ApplicationDbContext context)
        {
            Context = context;
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                var query = Context.Orders;
                var sql = query.ToQueryString();
                System.Diagnostics.Debug.WriteLine(sql);

                return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Stack Trace:");
                Console.WriteLine(ex.StackTrace);

                throw;
            }
        }

        public async Task ChangeOrderStatus(int orderId, Order.OrderStatus newStatus)
        {
            var order = await Context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Order with ID {orderId} not found.");
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await Context.Orders.FindAsync(orderId);
            if (order != null)
            {
                Context.Orders.Remove(order);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            try
            {
                return await Context.Users
                    .Include(u => u.Orders)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching users: {ex.Message}");
                return null;
            }
        }

        public async Task PlaceOrder(CustomerInfo customerInfo, string userId, List<Cart> CartItems)
        {
            foreach (var cartItem in CartItems)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = 4
                };

                Context.OrderDetails.Add(orderDetail);
            }

            await Context.SaveChangesAsync();

            var existingCustomerInfo = await Context.CustomerInfo.FirstOrDefaultAsync(ci => ci.UserId == customerInfo.UserId);
            if (existingCustomerInfo != null)
            {
                existingCustomerInfo.Name = customerInfo.Name;
            }
            else
            {
                customerInfo.UserId = userId;
                Context.CustomerInfo.Add(customerInfo);
            }

            await Context.SaveChangesAsync();

            var order = new Order
            {
                Ordernumber = GenerateOrderNumber(),
                PriceEstimate = 0,
                OrderDate = DateTime.Now,
                UserId = userId,
                CustomerInfoId = userId
            };

            Context.Orders.Add(order);
            await Context.SaveChangesAsync();


            var userCartItems = await Context.Carts.Where(ci => ci.UserId == userId).ToListAsync();

            foreach (var cartItem in userCartItems)
            {
                Context.Carts.Remove(cartItem);
            }

            await Context.SaveChangesAsync();
        }

        public int GenerateOrderNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

    }
}
