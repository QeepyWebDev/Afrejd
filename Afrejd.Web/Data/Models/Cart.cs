namespace Afrejd.Web.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Product CartProduct { get; set; }

        public List<Cart> CartItems { get; set; }

        public Cart() 
        {
            CartItems = new List<Cart>();
        }
    }
}
