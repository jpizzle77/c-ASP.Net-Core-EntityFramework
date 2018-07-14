using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BankAccounts.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
namespace BankAccounts.Controllers
{
    public class Transaction1Controller : Controller
    {
        private MyContext _context;
        public Transaction1Controller(MyContext context)
        {
            _context = context;
        }

        [HttpGet("account/{id}")]
        public IActionResult Index()
        {
            //Query the user (using the session id that was created upon registering or logging in)
            User current_user = _context.users
                                .Include(user => user.transactions)
                                //.OrderByDescending(user => user.transactions.OrderByDescending(transaction => transaction.created_at))
                                .SingleOrDefault(u => u.user_id == HttpContext.Session.GetInt32("id"));
            
           IQueryable<Transaction1> result =_context.users.SelectMany(user => user.transactions);
            IndexView model = new IndexView()
            {
                user_transaction = current_user,
                bank_transaction = new Transaction1(),
                all_transactions = result
            };
            return View(model);
          
        }

        [HttpPost("create")]
        public IActionResult Create(IndexView model)
        { 

            Transaction1 newTransaction = model.bank_transaction;
            newTransaction.user_id = model.user_transaction.user_id;
            
         
            model.user_transaction = _context.users
                                    .Include(u => u.transactions)
                                    .SingleOrDefault(user => user.user_id == newTransaction.user_id);
               
            _context.transactions.Add(newTransaction); // here you are adding the new Transaction 'newTransaction' to the transactions table in mysql
             newTransaction.current_balance = model.user_transaction.transactions.Sum(u => u.transaction_amount);
            _context.SaveChanges();
            return RedirectToAction("Index", "Transaction1", new {id=newTransaction.user_id});
        }
    }    
}