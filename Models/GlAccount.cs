using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class GlAccount
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Account name is required"), MaxLength(40)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Code")]
        public long CodeNumber { get; set; }     ///(1 for Assets, 2 for Liablities, 3 for Capital, 4 for Income and 5 for Expenses)! Are these categories??

        [Display(Name = "Account Balance")]
        //[Column(TypeName = "decimal(18,4)")]
        public decimal AccountBalance { get; set; }

        [Required(ErrorMessage = "Please select a GL category")]
        [Display(Name = "Category")]
        public int GLCategoryID { get; set; }

        public virtual GLCategory GlCategory { get; set; }

        [Required(ErrorMessage = "Please select a branch")]
        [Display(Name = "Branch")]
        public int BranchID { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
