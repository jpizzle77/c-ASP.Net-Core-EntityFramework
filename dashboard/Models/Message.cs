using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dashboard.Models

{
    public class Message
    {
        [Key]
        public long message_id { get; set; } 
        public long user_id { get; set; } 

        [Required]
        [Display(Name= "Message")]
        [MinLength(2, ErrorMessage="Message must be more than 3 charcters")]

        public string message { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Message()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }


    }

}