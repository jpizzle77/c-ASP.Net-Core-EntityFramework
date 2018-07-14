using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models

{
    public class User
    {
       
        [Key]
        public long user_id { get; set; } //NOTE: CHANGE TO USER_ID

        [Required]
        [Display(Name= "First Name")]
        [MinLength(2, ErrorMessage="First name must be more than 2 characters")]
        public string first_name { get; set; }

        [Required]
        [Display(Name= "Last Name")]
        [MinLength(2, ErrorMessage="Last name must be more than 2 charcters")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name= "Password")]
        [MinLength(3, ErrorMessage = "Password must be more than 3 characters")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<Transaction1> transactions {get;set;}

        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }

       
    }
    public class ConfirmUser : User
    {
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage="Password and Confirm Password must match")]
        [DataType(DataType.Password)]
        public string confirm_password { get; set; }
    }

    public class LoginUser
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string log_email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password must be more than 3 characters")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string log_password { get; set; }
    }


   
}