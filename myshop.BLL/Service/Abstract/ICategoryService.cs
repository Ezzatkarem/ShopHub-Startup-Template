using myshop.DAL.Models;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Abstract
{
    public interface ICategoryService
    {
        public Task AddCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int CategoryId);
        public Task<Category> EditCategoryAsync(Category category);
        public Task<IReadOnlyList<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(int id);
    }
}
