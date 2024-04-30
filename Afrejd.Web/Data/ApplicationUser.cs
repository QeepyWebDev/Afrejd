using Afrejd.Web.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Afrejd.Web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders;
        public ICollection<Product> Products;
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
    }

}
