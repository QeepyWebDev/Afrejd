namespace Afrejd.Web.Data.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
