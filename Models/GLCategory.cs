using App.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class GLCategory
    {
        [Key]
        public int CategoryId { get; set; }

    
        [Required(ErrorMessage = ("Category Name is required")), MaxLength(40)]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = ("Category Description is required")), MaxLength(140)]
        [DataType(DataType.Text)]
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }


        [Display(Name = "Code")]
        public long? CodeNumber { get; set; }

        [Display(Name = "GL Category Enabled")]
        public bool IsEnabled { get; set; } 

        [Display(Name = "Main Account Category")]
        [Required(ErrorMessage = "Please select a main GL Category")]
        public MainAccountCategory mainAccountCategory { get; set; }


        //public GLCategory()
        //{

        //}

    }

   
}
