using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using myshop.DAL.Models;
using myshop.DAL.Repo.Abstract;
using myshop.DAL.Repo.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Service.Implement
{
    public class ProductService : IproductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepo productRepo ;
        private readonly IMapper mapper;
        private readonly ICategoryRepo categoryRepo ;

        public ProductService(IUnitOfWork unitOfWork = null, IProductRepo ProductRepo = null, IMapper mapper = null, ICategoryRepo categoryRepo = null)
        {
            this.unitOfWork = unitOfWork;
            this.productRepo = ProductRepo;
            this.mapper = mapper;
            this.categoryRepo = categoryRepo;
        }


        public async Task AddProductAsync(ProductVM productVM, IFormFile file, string webRootPath)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                string uploadPath = Path.Combine(webRootPath, "Images", "Products");
                string extension = Path.GetExtension(file.FileName);

                using var stream = new FileStream(
                    Path.Combine(uploadPath, fileName + extension),
                    FileMode.Create);

                await file.CopyToAsync(stream);

                productVM.Product.Img = Path.Combine("Images", "Products", fileName + extension)
                    .Replace("\\", "/");
            }

            await productRepo.AddAsync(productVM.Product);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoryListAsync()
        {
            var categories = await categoryRepo.GetAllAsync();

            return categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
        public async Task<ProductVM> GetCreateProductVMAsync()
        {
            return new ProductVM
            {
                Product = new Product(),
                CategoryList = (await categoryRepo.GetAllAsync())
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
            };
        }
        public async Task<ProductVM> GetEditProductVMAsync(int id)
        {
            var product = await productRepo.GetByIdAsync(id);

            var categories = await categoryRepo.GetAllAsync();

            return new ProductVM
            {
                Product = product,
                CategoryList = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
        }

        public async Task EditProductAsync(ProductVM productVM, IFormFile? file, string webRootPath)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                string uploadPath = Path.Combine(webRootPath, "Images", "Products");
                string extension = Path.GetExtension(file.FileName);

                if (!string.IsNullOrEmpty(productVM.Product.Img))
                {
                    var oldImage = Path.Combine(webRootPath, productVM.Product.Img);

                    if (File.Exists(oldImage))
                    {
                        File.Delete(oldImage);
                    }
                }

                using var stream = new FileStream(
                    Path.Combine(uploadPath, fileName + extension),
                    FileMode.Create);

                await file.CopyToAsync(stream);

                productVM.Product.Img = Path.Combine("Images", "Products", fileName + extension)
                    .Replace("\\", "/");
            }

            await productRepo.UpdateAsync(productVM.Product);
            await unitOfWork.SaveChangesAsync();
        }
       

     

       
        public async Task<bool> DeleteProductAsync(int id, string webRootPath)
        {
            var product = await productRepo.GetByIdAsync(id);

            if (product == null)
                return false;

            if (!string.IsNullOrEmpty(product.Img))
            {
                var oldImage = Path.Combine(webRootPath, product.Img);

                if (File.Exists(oldImage))
                {
                    File.Delete(oldImage);
                }
            }

           await productRepo.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        async Task<IReadOnlyList<ProductListVM>> IproductService.GetAllAsync()
        {
            try
            {
                var res = await productRepo.GetProductswhisategoryAsync();
               var vm=mapper.Map<IReadOnlyList<ProductListVM>>(res);
                return vm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
