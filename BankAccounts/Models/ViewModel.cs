using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccounts.Models
{
    public class IndexView
    {
        public Transaction1 bank_transaction { get; set; }

        public ConfirmUser NewUser { get; set; }
        public LoginUser LogUser { get; set; }
        
        public User user_transaction { get; set; }
        public IQueryable all_transactions { get; set; }
      
         
        
    }
}