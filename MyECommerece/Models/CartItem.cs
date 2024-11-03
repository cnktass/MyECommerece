using System.ComponentModel.DataAnnotations;

namespace MyECommerece.Models
{
    public class CartItem
    {
        public int Id { get; set; }  

        [Required]
        public Product Product { get; set; }  

        [Required]
        public int Quantity { get; set; }  
    }

}
