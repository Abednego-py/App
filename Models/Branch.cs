using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Branch
    {
        public enum BranchStatus
        {
            Closed, Open
        }
        public int Id { get; set; }

        [Required]  
        [RegularExpression(pattern: @"^[A-Z][a-zA-Z]*$", ErrorMessage ="Please no special characters are allowed")]
        public string BranchName { get; set; }


        [Required]
        public string Address { get; set; }

        public long SortCode { get; set; }
        public BranchStatus Status { get; set; }
    }
}
