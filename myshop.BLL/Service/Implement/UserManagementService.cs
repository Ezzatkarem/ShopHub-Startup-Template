using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using myshop.DAL.Enums;
using myshop.DAL.Models;
using myshop.DAL.Repo.Abstract;
using myshop.DAL.Repo.Implementation;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Implement
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserManagementService(UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork = null)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task ChangeRoleAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            var role =await userManager.GetRolesAsync(user);
           var rolee=role.FirstOrDefault();
            if(rolee==Role.Customer.ToString())
            {

            
            await userManager.RemoveFromRoleAsync(user, rolee);
            await userManager.AddToRoleAsync(user, Role.Admin.ToString());
            }
            else
            {
                await userManager.RemoveFromRoleAsync(user, rolee);
                await userManager.AddToRoleAsync(user, Role.Customer.ToString());
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
           
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return ;

            await userManager.DeleteAsync(user);
        }

        public async Task<List<UserVM>> GetAllUsersAsync()
        {
            var users=  userManager.Users.ToList();
            var result = new List<UserVM>();

            foreach (var user in users)
            {
                var role = await userManager.GetRolesAsync(user);
                result.Add(new UserVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role.FirstOrDefault(),
                    IsLocked = user.LockoutEnd != null &&
                     user.LockoutEnd > DateTimeOffset.UtcNow
                });

            }
            return result;
        }

        public async Task LockUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await userManager.UpdateAsync(user);
        }

        public async Task UnlockUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            user.LockoutEnd = null;

            await userManager.UpdateAsync(user);
        }
    }
}
