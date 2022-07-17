using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models;
using App.Logic;
using App.Enums;
using App.ViewModels;
using System.Security.Claims;

namespace App.Controllers
{
    public class TellerPostingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        public TellerPostingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TellerPostings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TellerPosting.Include(t => t.CustomerAccount).Include(t => t.TillAccount).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TellerPostings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TellerPosting == null)
            {
                return NotFound();
            }

            var tellerPosting = await _context.TellerPosting
                .Include(t => t.CustomerAccount)
                .Include(t => t.TillAccount)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tellerPosting == null)
            {
                return NotFound();
            }

            return View(tellerPosting);
        }

        // GET: TellerPostings/Create
        public IActionResult Create()
        {
            ViewData["CustomerAccountID"] = new SelectList(_context.CustomerAccount, "Id", "AccountName");
            ViewData["GLAccountID"] = new SelectList(_context.GLAccount.Where(a => a.AccountName.ToLower() == "till").ToList(), "ID", " Code");
            //ViewData["GLAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: TellerPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Amount,Narration,Date,PostingType,CustomerAccountID,UserId,GLAccountID,Status")] TellerPosting tellerPosting)
        {
            if (!ModelState.IsValid)
            {
                string tellerId = tellerPosting.UserId;

                TellerPostingLogic telPostLogic = new TellerPostingLogic(_context);
                CustomerAccountLogic custActLogic = new CustomerAccountLogic(_context);

                bool tellerHasTill = _context.UserTill.Any(tu => tu.UserId.Equals(tellerId));
                if (!tellerHasTill)
                {
                    Problem("No till associated with logged in teller");
                    return View("NotFound");
                }
                // get user's till and do the necessary calculation

                int tillId = _context.UserTill.Where(tu => tu.UserId.Equals(tellerId)).First().GlAccountID;

                tellerPosting.GLAccountID = tillId;

                var tillAcct = _context.GLAccount.Find(tillId);

                var custAcct = _context.CustomerAccount.Find(tellerPosting.CustomerAccountID);

                tellerPosting.UserId = tellerId;

                tellerPosting.Date = DateTime.Now;

                var amt = tellerPosting.Amount;

                if (tellerPosting.PostingType == TellerPostingType.Withdrawal)
                {
                    if (custActLogic.CustomerAccountHasSufficientBalance(custAcct, amt))
                    {
                        if (!((decimal)tillAcct.AccountBalance >= amt))
                        {
                            Problem("Insufficient funds in till account");
                            return View("NotFound");
                        }
                        tellerPosting.Status = PostStatus.Approved;

                        string result = telPostLogic.PostTeller(custAcct, tillAcct, (float)amt, TellerPostingType.Withdrawal);
                        if (!result.Equals("success"))
                        {
                            Problem(result);
                            return View("NotFound");
                        }

                        _context.Entry(custAcct).State = EntityState.Modified;
                        _context.Entry(tillAcct).State = EntityState.Modified;

                        _context.Add(tellerPosting);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Problem("Insufficient funds in customer account");
                        return View("NotFound");
                    }

                }

                else
                {
                    tellerPosting.Status = PostStatus.Approved;

                    string result = telPostLogic.PostTeller(custAcct, tillAcct, (float)amt, TellerPostingType.Deposit);
                    if (!result.Equals("success"))
                    {
                        Problem(result);
                        return View("index");
                    }

                    _context.Entry(custAcct).State = EntityState.Modified;
                    _context.Entry(tillAcct).State = EntityState.Modified;

                    _context.Add(tellerPosting);
                    await _context.SaveChangesAsync();
                }


                    return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerAccountID"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", tellerPosting.CustomerAccountID);
            ViewData["GLAccountID"] = new SelectList(_context.GLAccount.Where(a => a.AccountName.ToLower() == "till").ToList(), "ID", "AccountName", tellerPosting.GLAccountID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", tellerPosting.UserId);
            return View(tellerPosting);
        }

        // GET: TellerPostings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TellerPosting == null)
            {
                return NotFound();
            }

            var tellerPosting = await _context.TellerPosting.FindAsync(id);
            if (tellerPosting == null)
            {
                return NotFound();
            }
            ViewData["CustomerAccountID"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", tellerPosting.CustomerAccountID);
            ViewData["GLAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", tellerPosting.GLAccountID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", tellerPosting.UserId);
            return View(tellerPosting);
        }

        // POST: TellerPostings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Amount,Narration,Date,PostingType,CustomerAccountID,UserId,GLAccountID,Status")] TellerPosting tellerPosting)
        {
            if (id != tellerPosting.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(tellerPosting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TellerPostingExists(tellerPosting.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerAccountID"] = new SelectList(_context.CustomerAccount, "Id", "AccountName", tellerPosting.CustomerAccountID);
            ViewData["GLAccountID"] = new SelectList(_context.GLAccount, "ID", "AccountName", tellerPosting.GLAccountID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", tellerPosting.UserId);
            return View(tellerPosting);
        }

        // GET: TellerPostings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TellerPosting == null)
            {
                return NotFound();
            }

            var tellerPosting = await _context.TellerPosting
                .Include(t => t.CustomerAccount)
                .Include(t => t.TillAccount)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tellerPosting == null)
            {
                return NotFound();
            }

            return View(tellerPosting);
        }

        // POST: TellerPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TellerPosting == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TellerPosting'  is null.");
            }
            var tellerPosting = await _context.TellerPosting.FindAsync(id);
            if (tellerPosting != null)
            {
                _context.TellerPosting.Remove(tellerPosting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TellerPostingExists(int id)
        {
          return (_context.TellerPosting?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        public IActionResult VaultIn()
        {
            return View();
        }


       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VaultIn(VaultInViewModel vaultInViewModel)
        {

            var glacct = _context.GLAccount.Where(a => a.AccountName.ToLower() == "vault").First();


            var tillAcct = _context.GLAccount.Where(a => a.CodeNumber == (long)vaultInViewModel.CodeNumber).First();

            if(glacct.AccountBalance > (float)vaultInViewModel.Amount)
            {
                tillAcct.AccountBalance += (float)vaultInViewModel.Amount;

                glacct.AccountBalance -= (float)vaultInViewModel.Amount;
            }
          

            return View(vaultInViewModel);
        }
        public async Task<IActionResult> VaultOut()
        {
            return View();
        }

        private string GetLoggedInUser()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
            
        }
    }
}
