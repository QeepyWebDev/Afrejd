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

        public async Task <List<Order>> GetAllOrders()
        {
            try
            {
                return await Context.Orders
                    .Where(o => !o.OrderConfirmed && !o.OrderDeclined)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ToListAsync();
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

        public async Task PlaceOrder(CustomerInfo customerInfo, string userId, List<Cart> cartItems)
        {
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();

            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = cartItem.ProductId
                };

                orderDetailsList.Add(orderDetail);
            }

            if (customerInfo.Id == 0)
            {
                customerInfo.UserId = userId;
                var existingCustomerInfo = await Context.CustomerInfo
                    .FirstOrDefaultAsync(ci => ci.UserId == userId);

                if (existingCustomerInfo != null)
                {
                    customerInfo = existingCustomerInfo;
                }
                else
                {
                    Context.CustomerInfo.Add(customerInfo);
                    await Context.SaveChangesAsync();
                }
            }

            var order = new Order
            {
                Ordernumber = GenerateOrderNumber(),
                PriceEstimate = 0,
                OrderDate = DateTime.Now,
                CustomerInfoId = customerInfo.Id,
                UserId = userId,

                OrderDetails = orderDetailsList
            };

            Context.Orders.Add(order);

            await Context.SaveChangesAsync();

            var userCartItems = await Context.Carts.Where(ci => ci.UserId == userId).ToListAsync();
            Context.Carts.RemoveRange(userCartItems);

            await Context.SaveChangesAsync();
        }

        public int GenerateOrderNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public async Task ConfirmOrder(int orderId)
        {
            var order = await Context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderConfirmed = true;
                order.OrderDeclined = false;
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeclineOrder(int orderId)
        {
            var order = await Context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderDeclined = true;
                order.OrderConfirmed = false;
                order.Status = Order.OrderStatus.Avslagen;

                await Context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetConfirmedOrders()
        {
            try
            {
                return await Context.Orders
                    .Where(o => o.OrderConfirmed)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching orders: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Order>> GetDeclinedOrders()
        {
            try
            {
                return await Context.Orders
                    .Where(o => o.OrderDeclined)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching orders: {ex.Message}");
                return null;
            }
        }

        public async Task UpdatePriceEstimate(int orderId, decimal? newPriceEstimate)
        {
            var order = await Context.Orders.FindAsync(orderId);
            if (order != null && newPriceEstimate.HasValue)
            {
                order.PriceEstimate = newPriceEstimate.Value;
                await Context.SaveChangesAsync();
            }
        }
    }
}
