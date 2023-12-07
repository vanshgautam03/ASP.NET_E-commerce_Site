namespace project.Models
{
    public class Cart
    {
        internal readonly object OrderId;

        public List<CartItem> cartItems {get; set;} = new List<CartItem>();
    }
}