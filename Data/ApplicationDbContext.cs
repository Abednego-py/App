using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<App.Models.Claims>? Claims { get; set; }
        public DbSet<App.Models.Customer>? Customer { get; set; }
        public DbSet<App.Models.GLCategory>? GLCategory { get; set; }
        public DbSet<App.Models.Branch>? Branch { get; set; }
        public DbSet<App.Models.GlAccount>? GlAccount { get; set; }
        public DbSet<App.Models.ApplicationRole>? ApplicationRole { get; set; }
    }
}