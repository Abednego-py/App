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

namespace App.Controllers
{
    public class CustomerAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerAccounts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerAccount.Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerAccount == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccount
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerAccount == null)
            {
                return NotFound();
            }

            return View(customerAccount);
        }

        // GET: CustomerAccounts/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName");
            return View();
        }

        // POST: CustomerAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,AccountName,AccountNumber,AccountBalance,Accounttype,DateOpened,IsActivated")] CustomerAccount customerAccount)
        {
            CustomerLogic customerLogic = new();

            CustomerAccountLogic customerAcctLogic = new(_context);
   
            var acctType = _context.CustomerAccount.Where(a => a.Accounttype == customerAccount.Accounttype).ToList();

            customerAccount.AccountNumber = Int64.Parse((customerAcctLogic.GenerateCustomerAccountNumber(customerAccount.Accounttype)).PadRight(5,'0') + (acctType.Count + 1).ToString() + customerAccount.CustomerID.ToString().PadLeft(5, '0'));

            customerAccount.DateOpened = DateTime.Now;

            if(customerAccount.AccountBalance == 0)
            {
                customerAccount.IsActivated = false;
            }
            else
            {
                customerAccount.IsActivated = true;
            }
            var customerData = _context.Customer.FindAsync(customerAccount.CustomerID); 

            if (!ModelState.IsValid)
            {
                _context.Add(customerAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName", customerAccount.CustomerID);
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerAccount == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccount.FindAsync(id);
            if (customerAccount == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName", customerAccount.CustomerID);
            return View(customerAccount);
        }

        // POST: CustomerAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerID,AccountName,AccountNumber,AccountBalance,Accounttype,DateOpened,IsActivated")] CustomerAccount customerAccount)
        {
            if (id != customerAccount.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerAccountExists(customerAccount.Id))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "FullName", customerAccount.CustomerID);
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerAccount == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccount
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerAccount == null)
            {
                return NotFound();
            }

            return View(customerAccount);
        }

        // POST: CustomerAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerAccount == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CustomerAccount'  is null.");
            }
            var customerAccount = await _context.CustomerAccount.FindAsync(id);
            if (customerAccount != null)
            {
                _context.CustomerAccount.Remove(customerAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerAccountExists(int id)
        {
          return (_context.CustomerAccount?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
