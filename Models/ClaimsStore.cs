using App.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Models
{
    public class ClaimsStore : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimsStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<Claim> AllClaims = new List<Claim>();

        public static List<Claim> GetClaims(ApplicationDbContext _context)
        {
            AllClaims = new List<Claim>();

            var claims = _context.Claims.ToList();

            foreach (var claim in claims)


            {
                if (AllClaims.Count == claims.Count)
                {
                    break;
                }
                
                var newClaim = new Claim(claim.ClaimsName, claim.ClaimsName);

                if (!AllClaims.Contains(newClaim))
                {
                    AllClaims.Add(newClaim);

                }
            }
            return AllClaims;
        }
    }
}
