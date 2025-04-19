using Microsoft.AspNetCore.Mvc;
using WebEcoMVC.Entity;
using WebEcoMVC.ViewModels;
namespace WebEcoMVC.ViewComponents
{
    public class MenuViewComponent : ViewComponent 
    {
        private readonly EcoMvcContext db;

        public MenuViewComponent(EcoMvcContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(loai => new MenuVM
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai,
                SoLuong = loai.HangHoas.Count
            }).OrderBy(p => p.TenLoai);

            //return View(data); // Defaut.cshtml
            return View("Defaut",data);
        }
    }
}
