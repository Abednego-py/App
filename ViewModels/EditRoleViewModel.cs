using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Claims = new List<string>();
        }
        public string? RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string? RoleName { get; set; }

        public List<string> Claims { get; set; }

        // public bool IsEnabled { get; set; }  

    }
}
