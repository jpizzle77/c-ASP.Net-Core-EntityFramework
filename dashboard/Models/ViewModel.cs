using System;
using System.Collections.Generic;

namespace dashboard.Models
{
    public class IndexView
    {
        public User User { get; set; }
        public ConfirmUser NewUser { get; set; }
        public LoginUser LogUser { get; set; }
        public List<User> Users { get; set; }

        

        public User Current_User { get; set; }


        public Message MessageUser { get; set; }
        
    
         
        
    }
}