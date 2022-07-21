using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class FinancialReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TrialBalance()
        {
            return View();
        }

        public IActionResult BalanceSheet()
        {
            return View();
        }

        public IActionResult ProfitandLoss()
        {
            return View();
        }
    }
}
