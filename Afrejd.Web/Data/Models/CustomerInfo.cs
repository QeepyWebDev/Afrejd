namespace Afrejd.Web.Data.Models
{
    public class CustomerInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public int Phone { get; set; }
        public string Company { get; set; }
        public string OrganizationNumber { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Order> Orders { get; set; }

        public CustomerInfo()
        {
            Country = "Sweden";
        }
    }
}
