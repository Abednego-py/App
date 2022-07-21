using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class ApplicationRole : IdentityRole
    {
        [Display(Name="Role Enabled")]
        public bool IsEnabled { get; set; }
    }
}
