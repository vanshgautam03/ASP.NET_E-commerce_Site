namespace project.Models
{
    public class Cart
    {
        public List<CartItem> CartItems {get; set;} = new List<CartItem>();
        
        internal readonly object OrderId;
    }
}