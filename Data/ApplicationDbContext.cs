using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

namespace project.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        // Change to be your model(s) and table(s)
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}