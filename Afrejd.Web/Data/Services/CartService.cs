using Afrejd.Web.Data.Interfaces;
using Afrejd.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Afrejd.Web.Data.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task AddItemToCart(int productId, string userId)
        {
            var existingCartItem = await Context.Carts
                .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);

                var product = await Context.Products.FindAsync(productId);
                if (product == null)
                {
                    throw new ArgumentException("Product not found.");
                }

                var cartItem = new Cart
                {
                    ProductId = productId,
                    UserId = userId,
                };

                try
                {
                    Context.Carts.Add(cartItem);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Exception occurred while saving changes: " + ex.Message);

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    }
                }
        }

        public async Task<List<Cart>> GetUserCartItems(string userId)
        {
            return await Context.Carts
		    .Include(c => c.CartProduct)
		    .Where(c => c.UserId == userId)
		    .ToListAsync();
		}

        public async Task RemoveItemFromCart(int productId, string userId)
        {
            var cartItem = await Context.Carts
                .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.UserId == userId);

            if (cartItem != null)
            {
                Context.Carts.Remove(cartItem);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("The product was not found in the cart.");
            }
        }

        public async Task<string> GetUserId()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            ClaimsPrincipal user = httpContext.User;

            Claim userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && user.Identity.IsAuthenticated)
            {
                string userId = userIdClaim.Value;
                return userId;
            }

            return null;
        }
    }
}
