using System.Security.Claims;

namespace App.Models
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
    {
        new Claim("Create new User", "Create new User"),
        new Claim("Edit User details","Edit User details"),
        new Claim("Create bank accounts","Create bank accounts"),
        new Claim("EOD", "Perform calculations for EOD")
    };
    }
}
