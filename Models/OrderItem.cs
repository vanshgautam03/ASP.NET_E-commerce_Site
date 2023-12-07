using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project.Models;
using Newtonsoft.Json;

namespace assignment1.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        public int OrderId { get; set; } = 0;

        [Required]
        public string ProductName { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 0.00M;

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}