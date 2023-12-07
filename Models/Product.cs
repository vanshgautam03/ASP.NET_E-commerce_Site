using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models 
{
    public class Product {
        [Key, Required]
        public int ItemId { get; set; } = 0;

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; } = 0;

        public string Name { get; set; } = String.Empty;

        [Required, StringLength(100)]
        public string BrandName { get; set; } = String.Empty;

        public string Size { get; set; } = String.Empty;

        public string Color { get; set; } = String.Empty;

        [StringLength(100)]
        public string? Type { get; set; } = String.Empty;

        public string ImageUrl { get; set; } = String.Empty;

        [StringLength(1000)]
        public string? Description { get; set; } = String.Empty;

        [Required]
        public int Price {get; set;} = 0;

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
        public int Id { get; internal set; }
    }
}