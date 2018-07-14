using System;
using System.Collections.Generic;

namespace Weddings.Models
{
    public class IndexView
    {

        public ConfirmUser NewUser { get; set; }
        public LoginUser LogUser { get; set; }

        

        public User Current_User { get; set; }
        public List<Wedding> Weddings { get; set; }
        public Wedding Wedding { get; set; }
        public Rsvp RSVP { get; set; }
      
         
        
    }
}