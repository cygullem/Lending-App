using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CyMvc.Entities;
using CyMvc.Models;
using CyMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CyMvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly CymvcContext _context;

        public PaymentController(CymvcContext payments)
        {
            _context = payments;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {   
            var loan = _context.Loans.FirstOrDefault(c => c.Id == id);

            if (loan == null)
            {
                return NotFound();
            }

            var payment = GetPayments(id);

 
            if (payment == null)
            {
                return NotFound();
            }


            ViewData["ClientId"] = payment.FirstOrDefault()?.ClientId;


            return View(payment);
        }


        [HttpPost]
        public IActionResult Index(PayModel pay)
        {

            Loan loan = _context.Loans.FirstOrDefault(l => l.Id == pay.LoanId);


            if (loan == null)
            {
                return NotFound();
            }


            loan.Collected += pay.Amount;
            loan.Collectable = Math.Max(loan.Collectable - pay.Amount, 0);


            _context.Loans.Update(loan);
            _context.SaveChanges();


            LogTransaction(pay);


            return RedirectToAction("Index", new { id = pay.LoanId });
        }

  
        private void LogTransaction(PayModel pay)
        {
            decimal tAmount = pay.Amount; 
            var payments = GetPayments(pay.LoanId); 

            foreach (var payment in payments)
            {
                if (tAmount <= 0)
                {
                    break; 
                }

                decimal amountToLog = Math.Min(tAmount, payment.Collectable);

                if (amountToLog > 0)
                {
                    var transaction = new Transaction
                    {
                        PaymentId = payment.PaymentId,
                        LoanId = pay.LoanId,
                        Amount = amountToLog,
                        Date = DateTime.Now 
                    };

                    _context.Transactions.Add(transaction);
                    tAmount -= amountToLog;
                }
            }

            _context.SaveChanges();
        }

        
        private List<PaymentViewModel> GetPayments(int loanID)
        {
            var paymentList =
               from paymentz in _context.Payments.Where(e => e.LoanId == loanID)
               join transaction in _context.Transactions
               on paymentz.Id equals transaction.PaymentId into pGroup
               from transaction in pGroup.DefaultIfEmpty()
               group transaction by paymentz into ptGroup
               select new PaymentViewModel
               {
                   PaymentId = ptGroup.Key.Id,
                   LoanId = ptGroup.Key.LoanId,
                   ClientId = ptGroup.Key.ClientId,
                   Collectable = ptGroup.Key.Collectable - ptGroup.Sum(t => t.Amount), 
                   CollectableOrig = ptGroup.Key.Collectable,
                   Date = ptGroup.Key.Schedule
               };

            return paymentList.ToList(); // Return list of payments as a List<PaymentViewModel>
        }
    }
}
