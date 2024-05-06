namespace Afrejd.Web.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Ordernumber { get; set; }
        public decimal ? PriceEstimate { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; }
        public string UserId { get; set; }
        public int OrderDetailsId { get; set; }
        public string CustomerInfoId { get; set; }

        public ApplicationUser User { get; set; }
        public Product Product { get; set; }

        public ICollection<OrderDetails> OrderDetails;



        public enum OrderStatus
        {
            Granskas,
            Hanteras,
            Genomfört
        }

        public Order()
        {
            Status = OrderStatus.Granskas;
        }
    }
}
