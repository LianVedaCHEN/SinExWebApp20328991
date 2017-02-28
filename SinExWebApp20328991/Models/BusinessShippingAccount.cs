using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    public class BusinessShippingAccount:ShippingAccount
    {
        [Required(ErrorMessage = "The Contact Personal Name is required")]
        [StringLength(40, ErrorMessage = "The length of fisrt name should be less than 40")]
        [Display(Name = "Contact Person Name")]
        public virtual string ContactPersonName { get; set; }

        [Required(ErrorMessage = "The Company Name is required")]
        [StringLength(40, ErrorMessage = "The length of fisrt name should be less than 40")]
        [Display(Name = "Company Name")]
        public virtual string CompanyName { get; set; }

  
        [StringLength(30, ErrorMessage = "The length of fisrt name should be less than 30")]
        [Display(Name = "Department Name")]
        public virtual string DepartmentName { get; set; }

    }
}