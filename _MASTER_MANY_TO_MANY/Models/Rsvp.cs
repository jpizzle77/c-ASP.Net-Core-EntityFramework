using System;
using System.ComponentModel.DataAnnotations;

namespace Weddings.Models

{
    public class Rsvp
    {
        [Key]
        public long rsvp_id { get; set; }
        public long user_id { get; set; }
        public long wedding_id { get; set; }
        public int user_action { get; set; }

        
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Rsvp()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }

    
}