using AutoMapper;
using Microsoft.CodeAnalysis.Options;
using WebEcoMVC.Entity;
using WebEcoMVC.ViewModels;

namespace WebEcoMVC.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();
            //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
            //.ReverseMap();
            CreateMap<InfoVM, KhachHang>().ReverseMap();
        }
    }
}
