using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinExWebApp20328991.Models
{
    public class PersonalShippingAccount:ShippingAccount
    {
        [Required(ErrorMessage = "The Email field is required")]
        [StringLength(35, ErrorMessage = "The length of fisrt name should be less than 35")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }
        [Required(ErrorMessage = "The Email field is required")]
        [StringLength(35, ErrorMessage = "The length of last name should be less than 35")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

    }
}