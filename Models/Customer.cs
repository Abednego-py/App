using App.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        [StringLength(225, ErrorMessage = "Customer's name can not be less than 7 letters", MinimumLength = 7)]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Full name should only contain characters and white spaces")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(225)]
        [Display(Name = "Address")]
        [MinLength(4)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, ErrorMessage = "Telephone Number cannot be less than 11 letters", MinimumLength = 11)]
        [Display(Name = "Telephone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(225, ErrorMessage = "Email Address cannot be less than 9 letters", MinimumLength = 9)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Customer Enabled")]
        public bool IsActivated { get; set; }

    }
}

