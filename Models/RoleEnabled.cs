namespace App.Models
{
    public class RoleEnabled
    {
        public int Id { get; set; }

        public int AspNetRolesId { get; set; }  
        public bool IsEnabled { get; set; }
    }
}
