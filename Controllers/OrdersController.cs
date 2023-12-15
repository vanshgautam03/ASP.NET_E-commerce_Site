using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using project.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using assignment1.Models;
namespace project.Models
{
    public class OrdersController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        public OrdersController(CartService cartService, ApplicationDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        [Authorize()]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = _cartService.GetCart();

            if (cart == null)
            {
                return NotFound();
            }


            var order = new Order
            {
                /*hear some errors not match with sir code*/

                UserId = userId,
                //  Total = cart.cartItems.Sum(cartItem => cartItem.Quantity * cartItem.Product.Price)
                // OrderItems = new List<OrderItem>() v create
            };

            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cart.cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductName = cartItem.Product.Name,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price
                };

                orderItems.Add(orderItem);
            }

            order.OrderItems = orderItems;
            order.Total = cart.cartItems.Sum(cartItem => (decimal)(cartItem.Quantity * cartItem.Product.Price));

            return View("Details", order);
        }
    }
}