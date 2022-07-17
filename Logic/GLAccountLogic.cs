using App.Data;
using App.Enums;
using App.Models;

namespace App.Logic
{
    public class GLAccountLogic
    {
        private readonly ApplicationDbContext _context;

        public GLAccountLogic(ApplicationDbContext context)
        {
            this._context = context;
        }
        public List<GLAccount> GetByMainCategory(MainAccountCategory mainCategory)
        {
            return _context.GLAccount.Where(a => a.GLCategory.mainAccountCategory == mainCategory).ToList();
        }
    }
}
