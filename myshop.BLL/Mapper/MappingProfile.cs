using AutoMapper;
using myshop.BLL.ViewModels;
using myshop.DAL.Models;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Mapper
{
    public class MappingProfile :Profile
    {
       public MappingProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<Product, ProductListVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ApplicationUser, RegisterVM>();
            CreateMap<RegisterVM, ApplicationUser>()
    .ForMember(d => d.UserName,
        o => o.MapFrom(s => s.Email))
    .ForMember(d => d.Email,
        o => o.MapFrom(s => s.Email))
    .ForMember(d => d.Name,
        o => o.MapFrom(s => s.FullName));

        }

    }
}
