using System;
using System.ComponentModel.DataAnnotations;

namespace RESTauranter.Models

{
    public class Review

    {
        [Key]
        public long review_id { get; set; }

        [Required]
        [Display(Name= "Reviewer's Name")]
        [MinLength(2, ErrorMessage="Reviewer's name must be more than 2 characters")]
        public string reviewer_name { get; set; }

        [Required]
        [Display(Name= "Restaurant's Name")]
        [MinLength(2, ErrorMessage="Restaurant's name must be more than 2 characters")]
        public string restaurant_name { get; set; }

        [Required]
        [Display(Name= "Review")]
        [MinLength(2, ErrorMessage="Review must be more than 2 characters")]
        public string review { get; set; }

        [Required]
        [Display(Name= "Date of Visit")]
        public DateTime date_of_visit { get; set; }

        [Required]
        [Display(Name= "Rating")]
        public string rating { get;set; }

        

        

    }
}