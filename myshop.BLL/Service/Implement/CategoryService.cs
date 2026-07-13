using myshop.BLL.Service.Abstract;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork  ;
        private readonly ICategoryRepo categoryRepo;

        public CategoryService( IUnitOfWork unitOfWork = null, ICategoryRepo categoryRepo = null)
        {
            this.unitOfWork = unitOfWork;
            this.categoryRepo = categoryRepo;
        }

        public async Task AddCategoryAsync(DAL.Models.Category category)
        {
            try
            {
                await unitOfWork.Categories.AddAsync(category);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int CategoryId)
        {
            try
            {
                var res = await categoryRepo.GetByIdAsync(CategoryId);
                if (res == null)
                    return false;
                categoryRepo.DeleteAsync(res);
                unitOfWork.SaveChangesAsync();
                return true;
                    
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Category?> EditCategoryAsync(Category category)
        {
            var categoryInDb = await categoryRepo.GetByIdAsync(category.Id);

            if (categoryInDb == null)
                return null;

            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;

           await categoryRepo.UpdateAsync(categoryInDb);
            await unitOfWork.SaveChangesAsync();

            return categoryInDb;
        }
        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            try
            {
                var res = await categoryRepo.GetAllAsync();

                if (res == null)
                    return null;

                return res;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            try
            {
                var res = await categoryRepo.GetByIdAsync(id);

                if (res == null)
                    return null;

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    
    }
}
