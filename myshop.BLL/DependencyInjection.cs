using Microsoft.Extensions.DependencyInjection;
using myshop.BLL.Service.Abstract;
using myshop.BLL.Service.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSevice(this IServiceCollection services)
        {
            services.AddScoped<IproductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            return services;
        }
    }
}
