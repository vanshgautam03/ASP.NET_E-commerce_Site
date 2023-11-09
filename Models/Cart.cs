using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class Cart
    {
        public List<CartItem> cartItems {get; set;} = new List<CartItem>();
    }
}