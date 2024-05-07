using Afrejd.Web.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Afrejd.Web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products;
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
        public ICollection<CustomerInfo> CustomerInfo { get; set; }

        public ApplicationUser()
        {
            Orders = new List<Order>();
            Products = new List<Product>();
        }

    }

}
