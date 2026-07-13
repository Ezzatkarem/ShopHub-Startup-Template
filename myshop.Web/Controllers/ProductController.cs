using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using myshop.BLL.Service.Abstract;
using myshop.BLL.ViewModels;
using myshop.DAL.Models;
using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller 
    {
   // private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IproductService productService;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IproductService productService)
        {
          //  _context = context;
            _webHostEnvironment = webHostEnvironment;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
           
            var products = await productService.GetAllAsync();


            return Json(new { data = products });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = await productService.GetCreateProductVMAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                productVM.CategoryList = await productService.GetCategoryListAsync();
                return View(productVM);
            }

            await productService.AddProductAsync(productVM, file, _webHostEnvironment.WebRootPath);

            TempData["Create"] = "Item has Created Successfully";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await productService.GetEditProductVMAsync(id);

            if (vm == null || vm.Product == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                productVM.CategoryList = await productService.GetCategoryListAsync();
                return View(productVM);
            }

            await productService.EditProductAsync(productVM, file, _webHostEnvironment.WebRootPath);

            TempData["Update"] = "Data has Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productService.DeleteProductAsync(id, _webHostEnvironment.WebRootPath);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }

            return Json(new
            {
                success = true,
                message = "Deleted successfully"
            });
        }

    }
}
