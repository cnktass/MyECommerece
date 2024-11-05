using System.ComponentModel.DataAnnotations.Schema;

namespace MyECommerece.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }= "Pending";
        public ICollection<OrderItem>OrderItems { get; set; }
    }
}
