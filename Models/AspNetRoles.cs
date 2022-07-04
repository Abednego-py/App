using Microsoft.AspNetCore.Identity;

namespace App.Models
{
    public class AspNetRoles : IdentityRole
    {
        public bool IsEnabled { get; set; } 
    }
}
