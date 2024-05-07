namespace Afrejd.Web.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Ordernumber { get; set; }
        public decimal ? PriceEstimate { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Granskas;
        public string UserId { get; set; }
        public int CustomerInfoId { get; set; }
        public bool OrderConfirmed { get; set; } = false;

        public ApplicationUser User { get; set; }
        public CustomerInfo CustomerInfo { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }



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
