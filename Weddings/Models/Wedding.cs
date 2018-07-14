using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weddings.Models

{
    public class Wedding
    {
        [Key]
        public long wedding_id { get; set; }
        public long user_id { get; set; }

        [Required]
        [Display(Name= "Wedder One")]
        [MinLength(2, ErrorMessage="Wedder One name must be more than 2 characters")]
        public string wedder_one { get; set; }

        [Required]
        [Display(Name= "Wedder Two")]
        [MinLength(2, ErrorMessage="Wedder Two name must be more than 2 charcters")]
        public string wedder_two{ get; set; }

       [Required]
       [Display(Name= "Date of Wedding")]
       public DateTime date_of_wedding { get; set; }

       [Required]
       [Display(Name= "Wedding Address")]

       public string wedding_address { get; set; }
       public List<Rsvp> wedding_rsvps { get; set; }


        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public Wedding()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }

}