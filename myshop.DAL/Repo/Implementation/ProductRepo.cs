using Microsoft.EntityFrameworkCore;
using myshop.DAL.Models;
using myshop.DAL.Repo.Abstract;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repo.Implementation
{
    public class ProductRepo : GenerecRepo<Product>, IProductRepo
    {
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Product>> GetProductswhisategoryAsync()
        {
            return await _context.Products.Include(c=>c.Category).ToListAsync();
        }
    }
}
