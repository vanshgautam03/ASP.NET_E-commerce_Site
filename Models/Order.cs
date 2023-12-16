using System.ComponentModel.DataAnnotations;
using assignment1.Models;
using Microsoft.AspNetCore.Identity;

namespace project.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        public string UserId { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; } = 0.00M;

        [Required]
        public bool PaymentReceived {get; set; } = false;

        public IdentityUser? User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}