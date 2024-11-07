using Microsoft.AspNetCore.Mvc;
using MyECommerece.Data;
using MyECommerece.Models;
using MyECommerece.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> CreateOrder()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Redirect("/Identity/Account/Login");
        }

        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
        if (cart == null || !cart.Any())
        {
            return RedirectToAction("ViewCart", "Cart");
        }
        var order = new Order
        {
            ApplicationUserId = user.Id,
            TotalAmount = cart.Sum(c => c.Product.Price * c.Quantity),
            OrderItems = cart.Select(c => new OrderItem
            {
                ProductId = c.Product.Id, 
                Quantity = c.Quantity,
                Price = c.Product.Price
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        var invoice = new Invoice
        {
            OrderId = order.Id,
            InvoiceDate = DateTime.Now,
            TotalAmount = order.TotalAmount,
            BillingAddress = user.Address,
            PhoneNumber = user.PhoneNumber
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        HttpContext.Session.Remove("Cart");//Sessiondaki islemler bitttikten sonra sepetteki urunleri sildik
        return RedirectToAction("OrderDetails", new { id = order.Id });
    }
    public IActionResult OrderDetails(int id)
    {
        var order = _context.Orders.Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
    public IActionResult InvoiceDetails(int id)
    {
        var invoice = _context.Invoices.Include(i => i.Order)
            .ThenInclude(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefault(i => i.OrderId == id);

        if (invoice == null)
        {
            return NotFound();
        }

        return View(invoice);
    }

}
