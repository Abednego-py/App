using App.Models;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class VaultInViewModel
    {

        //[DataType(DataType.Currency)]
        //[RegularExpression(@"^[0-9.]+$", ErrorMessage = "Invalid amount"), Range(1, (double)Decimal.MaxValue, ErrorMessage = ("Amount must be between 1 and a reasonable maximum value"))]
        [Required]
        public decimal Amount { get; set; }

       public long CodeNumber { get; set; }  

    }
}
