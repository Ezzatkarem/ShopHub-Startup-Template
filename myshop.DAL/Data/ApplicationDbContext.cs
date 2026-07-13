
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myshop.Entities.Models;

namespace myshop.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        public DbSet<DAL.Models.Category> Categories { get; set; }
        public DbSet<DAL.Models.Product> Products { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }


    }
}
