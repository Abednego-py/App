using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(255)]
        [Required(ErrorMessage = "Last Name field is required")]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Full name should contain characters and white spaces only"), MaxLength(40)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Last Name field is required")]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Full name should contain characters and white spaces only"), MaxLength(40)]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }


        [Display(Name = "Role Enabled")]
        public bool IsEnabled { get; set; } 
    }
}
