using System.ComponentModel.DataAnnotations;
using assignment1.Models;

namespace project.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required, StringLength(300)]
        public string Name { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? Description { get; set; } = String.Empty;

        public virtual ICollection<Product>? Products { get; set; } 
    }
}