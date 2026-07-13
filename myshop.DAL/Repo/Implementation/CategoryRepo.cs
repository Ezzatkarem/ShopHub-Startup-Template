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
    public class CategoryRepo : GenerecRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
