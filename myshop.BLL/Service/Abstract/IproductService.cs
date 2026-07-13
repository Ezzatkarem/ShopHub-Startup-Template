using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using myshop.BLL.ViewModels;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Abstract
{
    public interface IproductService
    {
        Task AddProductAsync(ProductVM productVM, IFormFile file, string webRootPath);
        Task<ProductVM> GetCreateProductVMAsync();
        Task<ProductVM> GetEditProductVMAsync(int id);
        Task<bool> DeleteProductAsync(int id, string webRootPath);
        Task EditProductAsync(ProductVM productVM, IFormFile? file, string webRootPath);
        Task<IEnumerable<SelectListItem>> GetCategoryListAsync();
        //public Task<Category> EditProductAsync(Category category);
        public Task<IReadOnlyList<ProductListVM>> GetAllAsync();
      
    }
}
