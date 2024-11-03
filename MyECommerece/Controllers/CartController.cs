using Microsoft.AspNetCore.Mvc;
using MyECommerece.Helpers;
using MyECommerece.Models;
using MyECommerece.Data;
using System.Linq;
using System.Collections.Generic;

namespace MyECommerece.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.Product.Id == product.Id);
            if (cartItem == null)
            {
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index", "Product"); 
        }
        public IActionResult ViewCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }
    }
}
