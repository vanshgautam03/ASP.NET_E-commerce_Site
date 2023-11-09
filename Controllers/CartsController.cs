using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment1.Controllers
{
    public class CartsController : Controller
    {
        private readonly string _cartSessionKey;
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context){
            _context = context;
            _cartSessionKey = "cart";
        }

        public async Task<IActionResult> Index(){
            // get our cart
            var cart = GetCart();

            if(cart == null)
            {
                return NotFound();
            }
            // if the cart has items , we need to add the product
            if(cart.cartItems.Count > 0){
                foreach(var cartItem in cart.cartItems){
                    var product = await _context.Products
                        .Include(p => p.Department)
                        .FirstOrDefaultAsync(p => p.ItemId == cartItem.ProductId);
                    if(product != null){
                        cartItem.Product = product;
                    }
                }
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int ProductId, int Quantity){
            var cart = GetCart();
            if (cart == null)
            {
                return NotFound();
            }
            var cartItem = cart.cartItems.Find(cartItem => cartItem.ProductId == ProductId);

            if (cartItem != null && cartItem.Product != null)
            {
                cartItem.Quantity += Quantity; 
            }    
            else
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.ItemId == ProductId);

                if (product == null) {
                    return NotFound();
                }

                cartItem = new CartItem { ProductId = ProductId, Quantity = Quantity, Product = product };
                cart.cartItems.Add(cartItem);
            }


            SaveCart(cart);

            return RedirectToAction("Index");
        }

        private Cart? GetCart(){
            var cartJson = HttpContext.Session.GetString(_cartSessionKey);
            return cartJson == null? new Cart() : JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        private void SaveCart(Cart cart){
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(_cartSessionKey, cartJson);
        }
    }
}