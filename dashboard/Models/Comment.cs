using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dashboard.Models

{
    public class Comment
    {
        [Key]
        public long comment_id { get; set; } 
        public long user_id { get; set; } 
        public long message_id { get; set; } 
        

        [Required]
        [Display(Name= "Comment")]
        [MinLength(2, ErrorMessage="Comment must be more than 3 charcters")]

        public string comment { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Comment()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

    }
}