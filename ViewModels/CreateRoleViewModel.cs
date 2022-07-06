using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        //public string NormalizedName { get; set; }

        public bool IsEnabled { get; set; }

    }
}





