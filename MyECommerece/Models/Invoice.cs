using System.ComponentModel.DataAnnotations.Schema;

namespace MyECommerece.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal TotalAmount { get; set; }
        public string BillingAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Order Order { get; set; }    
    }
}
