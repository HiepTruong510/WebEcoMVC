using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebEcoMVC.Entity;
using WebEcoMVC.Helpers;
using WebEcoMVC.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebEcoMVC.Controllers
{
    public class ProfileController : Controller
    {

        private readonly EcoMvcContext db;
        private readonly IMapper _mapper;

        public ProfileController(EcoMvcContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            string id ="";
            if (User.Identity.IsAuthenticated)
            {
                id = User.FindFirst(Variable.CLAIM_CUSTOMERID)?.Value;
            }
            var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == id);
            if (khachHang == null)
            {
                TempData["Message"] = $"không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var model = _mapper.Map<InfoVM>(khachHang);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(InfoVM model, IFormFile Hinh)
        {
            string id = HttpContext.Session.GetString("MaKH");
            var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == id);
            ModelState.Remove("Hinh");
            if (ModelState.IsValid)
            {
                try
                {
                   _mapper.Map(model, khachHang);

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }
                   

                    db.Update(khachHang);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Profile");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View("Index");
        }
    }
}
