using myshop.DAL.Models;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repo.Abstract
{
    public interface IProductRepo :IGenerecRepo<Product>
    {
        public Task <IReadOnlyList<Product>> GetProductswhisategoryAsync ();
    }
   
}
