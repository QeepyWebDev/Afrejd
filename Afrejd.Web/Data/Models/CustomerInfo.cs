namespace Afrejd.Web.Data.Models
{
    public class CustomerInfo
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int ? PostalCode { get; set; }
        public string Country { get; set; } = string.Empty;
        public int ? Phone { get; set; }
        public string Company { get; set; } = string.Empty;
        public string OrganizationNumber { get; set; } = string.Empty;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; }

        public CustomerInfo()
        {
            Country = "Sweden";
        }
    }
}
