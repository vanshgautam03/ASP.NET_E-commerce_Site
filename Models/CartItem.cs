using assignment1.Models;

namespace project.Models
{
    public class CartItem
    {
        public int ProductId{ get; set;}
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Product Product{get; set;} = new Product();
    }
}