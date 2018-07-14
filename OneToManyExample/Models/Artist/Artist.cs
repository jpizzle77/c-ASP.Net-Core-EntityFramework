using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneToManyExample.Models
{
    public class Artist
    {
        [Key]
        public long artist_id { get; set; }
        [Required]
        public string name { get; set; } 
        
        public List<Album> Albums {get;set;}
    }
   
}