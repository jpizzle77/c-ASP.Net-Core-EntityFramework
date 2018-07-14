using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models

{
    public class Transaction1
    {
        [Key]
        public long transaction_id { get; set; } 
        public int current_balance { get; set; }
        public int transaction_amount { get; set; }
        public long user_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public User User { get; set; }

        public Transaction1()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}