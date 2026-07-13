using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Service.Abstract;
using myshop.DAL.Models;
using myshop.DAL.Repo.Abstract;
using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize (Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService categoryService;

        public CategoryController(ApplicationDbContext context, ICategoryService categoryService)
        {
            _context = context;
            this.categoryService = categoryService;
        }

        public async Task< IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task< IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
               await categoryService.AddCategoryAsync(category);
                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task< IActionResult> Edit(int id)
        {

            var categoryIndb = await categoryService.GetByIdAsync(id);

            if (categoryIndb == null)
                return NotFound();

            return View(categoryIndb);
        }

        [HttpPost]
        public async Task< IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryIndb = await categoryService.EditCategoryAsync(category);

                return View(categoryIndb);
                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task< IActionResult> Delete(int id)
        {

            var categoryIndb =await categoryService.GetByIdAsync(id);

            return View(categoryIndb);
        }

        [HttpPost]
        public async Task< IActionResult> DeleteCategory(int id)
        {
           
           await categoryService.DeleteCategoryAsync(id);
           
            TempData["Delete"] = "Item has Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
