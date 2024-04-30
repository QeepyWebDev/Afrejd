namespace Afrejd.Web.Data.Models
{
    public class ConfirmedOrder
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
