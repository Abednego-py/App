namespace App.ViewModels
{
    public class RoleClaimViewModel
    {
         public RoleClaimViewModel()
        {
            Claims = new List<RoleClaims>();

        } 

        public string RoleId { get; set; }  
        public List<RoleClaims> Claims { get; set; }

    }
}
