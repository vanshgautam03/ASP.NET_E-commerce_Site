using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assignment1.Models;
using assignment1.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using project.Models;

namespace assignment1.Models
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

            if (cart == null) {
                return NotFound();
            }

            
            var order = new Order {
                UserId = userId
            };

            var orderItems = new List<OrderItem>();

            foreach(var cartItem in cart.cartItems)
            {
                var orderItem = new OrderItem {
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