using Microsoft.AspNetCore.Mvc;
using WebEcoMVC.Entity;
using WebEcoMVC.ViewModels;
using WebEcoMVC.Entity;
using AutoMapper;
using WebEcoMVC.Helpers;

namespace WebEcoMVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly EcoMvcContext db;
        private readonly IMapper _mapper;

        public RegisterController(EcoMvcContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRamdomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;//sẽ xử lý khi dùng Mail để active
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }

                    db.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Shop");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message} shh";
                }
            }
            return View();
        }


    }
}
