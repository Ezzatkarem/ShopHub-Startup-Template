using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using myshop.DAL.Enums;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(LoginVM VM)
        {
            var user = await userManager.FindByEmailAsync(VM.Email);

            if (user == null)
                return false;

            if (!await userManager.IsEmailConfirmedAsync(user))
                return false;

            var result = await signInManager.PasswordSignInAsync(
                user,
                VM.Password,
                VM.RememberMe,
                lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task LogOUtAsync()
        {
            await signInManager.SignOutAsync();


        }

        public async Task<bool> Registr(RegisterVM VM)
        {
            var exist = await userManager.FindByEmailAsync(VM.Email);

            if (exist != null)
                return false;

            var user = mapper.Map<ApplicationUser>(VM);

            var result = await userManager.CreateAsync(user, VM.Password);

            if (!result.Succeeded)
                return false;

            await userManager.AddToRoleAsync(user, "Customer");

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);



            var request = httpContextAccessor.HttpContext.Request;

            var link =
                $"{request.Scheme}://{request.Host}/Account/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";
            await emailService.SendEmailAsync(
    user.Email,
    "Confirm your email",
    $@"
<h2>Welcome to MyShop</h2>
<p>Click the link below to confirm your email:</p>
<a href='{link}'>Confirm Email</a>");
            return true;
        }
    }
}
