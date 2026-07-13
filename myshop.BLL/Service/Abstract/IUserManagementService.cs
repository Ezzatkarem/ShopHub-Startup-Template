using myshop.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Abstract
{
    public interface IUserManagementService
    {
        Task<List<UserVM>> GetAllUsersAsync();

        Task  ChangeRoleAsync(string userId);

        Task LockUserAsync(string userId);

        Task UnlockUserAsync(string userId);

        Task DeleteUserAsync(string userId);
    }
}
