using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<App.Models.Claims>? Claims { get; set; }
        public DbSet<App.Models.GLCategory>? GLCategory { get; set; }
        public DbSet<App.Models.Branch>? Branch { get; set; }
     
        public DbSet<App.Models.ApplicationRole>? ApplicationRole { get; set; }
    
        public DbSet<App.Models.GLAccount>? GLAccount { get; set; }
    
        public DbSet<App.Models.UserTill>? UserTill { get; set; }
    
        public DbSet<App.Models.AccountConfiguration>? AccountConfiguration { get; set; }
    
        public DbSet<App.Models.GlPosting>? GlPosting { get; set; }
    
        public DbSet<App.Models.Customer>? Customer { get; set; }
    
        public DbSet<App.Models.CustomerAccount>? CustomerAccount { get; set; }
    
        public DbSet<App.Models.TellerPosting>? TellerPosting { get; set; }
    
        public DbSet<App.Models.Transaction>? Transaction { get; set; }
    
        public DbSet<App.Models.LoanAccount>? LoanAccount { get; set; }
    

   
    }
}