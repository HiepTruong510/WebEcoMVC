using WebEcoMVC.Helpers;
using WebEcoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebEcoMVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(Variable.CART_KEY) ?? new List<CartItem>();

            return View("CartPanel", new CartVM
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            });
        }
    }
}
