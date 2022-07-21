using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Claims 
    {
        public int ClaimsId { get; set; }

        [Display(Name = "Claims Name")]
        [Required]
        public string ClaimsName { get; set; }

        public Claims()
        {

        }
    }
}
