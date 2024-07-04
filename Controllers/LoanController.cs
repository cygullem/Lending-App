using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CyMvc.Entities;
using CyMvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace CyMvc.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly CymvcContext _context;

        public LoanController(CymvcContext Loan)
        {
            _context = Loan;
        }

        public async Task<IActionResult> Index()
        {   

            var clientInfos = await _context.ClientInfos
                .Select(clientInfo => new ClientInfoViewModel
                {
                    Id = clientInfo.Id,
                    FirstName = clientInfo.FirstName,
                    LastName = clientInfo.LastName,
                    Address = clientInfo.Address,
                    ZipCode = clientInfo.ZipCode,
                    Birthday = clientInfo.Birthday,
                    Age = clientInfo.Age,
                    NameOfFather = clientInfo.NameOfFather,
                    NameOfMother = clientInfo.NameOfMother,
                    CivilStatus = clientInfo.CivilStatus,
                    Religion = clientInfo.Religion,
                    Occupation = clientInfo.Occupation,
                })
                .ToListAsync();

            return View(clientInfos);
        }

        [HttpGet]
        public IActionResult Loan(int id)
        {
            var client = _context.ClientInfos.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            var loan = _context.Loans.Where(e => e.ClientId == id).ToList();
            if (loan == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = id;
            return View(loan);
        }


        [HttpPost]
        public IActionResult Loan(Loan loan)
        {
            loan.Collectable = loan.Amount + loan.InterestAmount;
            loan.TotalPayable = loan.Collectable;

            _context.Loans.Add(loan);
            _context.SaveChanges();

            GenerateSchedule(loan);

            return RedirectToAction("Loan", new { id = loan.ClientId });
        }

         private void GenerateSchedule(Loan loan)
        {
            int numberOfSchedules = loan.Term;
            // Determine the interval in days based on the loan type
            var intervalDays = loan.Type.ToLower() switch
            {
                "daily" => 1,
                "weekly" => 7,
                "biweekly" => 15,
                "monthly" => 30,
                _ => throw new ArgumentException("Invalid loan type"),
            };

            // Generate payment schedules
            for (int i = 0; i < numberOfSchedules; i++)
            {
                var schedule = new Payment
                {
                    LoanId = loan.Id,
                    ClientId = loan.ClientId,
                    Schedule = loan.DateCreated.AddDays(intervalDays * (i + 1)),
                    Collectable = Math.Round(loan.TotalPayable / numberOfSchedules),
                };
                // Add the schedule to the database and save changes
                _context.Payments.Add(schedule);
                _context.SaveChanges();
            }
        }

        public IActionResult ViewTransaction(int id)
        {

            var transactionsList = _context.Transactions.Where(c => c.LoanId == id).ToList();

            return PartialView("Views/Loan/_Transaction.cshtml", transactionsList);
        }
    }
}
