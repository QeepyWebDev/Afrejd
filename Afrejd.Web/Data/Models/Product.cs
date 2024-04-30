using System.ComponentModel.DataAnnotations.Schema;

namespace Afrejd.Web.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
