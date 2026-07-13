using AutoMapper;
using Microsoft.AspNetCore.Identity;
using myshop.BLL.ViewModels;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Abstract
{
    public interface IUserService
    {
        Task<bool> Registr(RegisterVM VM);
        Task<bool> LoginAsync(LoginVM VM);
        Task LogOUtAsync();


    }
}
