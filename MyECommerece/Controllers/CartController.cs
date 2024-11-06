using Microsoft.AspNetCore.Mvc;
using MyECommerece.Helpers;
using MyECommerece.Models;
using MyECommerece.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyECommerece.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> ViewCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.Address = user.Address;
            return View(cart);

        }
        public async Task<IActionResult> UpdateUserInfo(string phoneNumber, string address)
        {
            var user = await _userManager.GetUserAsync (User);
            if (user == null) 
            {
                return Redirect("/Identity/Account/Login");
            }
            user.PhoneNumber = phoneNumber;
            user.Address = address;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("ViewCart");
        }
    }
}
