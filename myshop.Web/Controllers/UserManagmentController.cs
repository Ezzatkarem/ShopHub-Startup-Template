using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
    public class UserManagmentController : Controller
    {
        private readonly IUserManagementService userManagementService;

        public UserManagmentController(IUserManagementService userManagementService)
        {
            this.userManagementService = userManagementService;
        }

        public async Task< IActionResult> Index()
        {
            var users = await userManagementService.GetAllUsersAsync();

            return View(users);
        }
        public async Task<IActionResult> ChangeRole(string Id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == Id)
            {
                TempData["Success"] = "Can't Chinge Role of This User.";
                return RedirectToAction(nameof(Index));

            }
            await userManagementService.ChangeRoleAsync(Id);
            TempData["Success"] = "User role updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(currentUserId== Id)
            {
                TempData["Success"] = "Can't Delete This User.";
                return RedirectToAction(nameof(Index));

            }
             await userManagementService.DeleteUserAsync(Id);
            TempData["Success"] = "User deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Lock(string Id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == Id)
            {
                TempData["Success"] = "Can't Lock This User.";
                return RedirectToAction(nameof(Index));

            }
           await  userManagementService.LockUserAsync(Id);

            TempData["Success"] = "User locked successfully.";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unlock(string Id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == Id)
            {
                TempData["Success"] = "Can't UnLock This User.";
                return RedirectToAction(nameof(Index));

            }
             await userManagementService.UnlockUserAsync(Id);
            TempData["Success"] = "User unlocked successfully.";

            return RedirectToAction(nameof(Index));
        }



    }
}
