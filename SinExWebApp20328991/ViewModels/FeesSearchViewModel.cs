using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SinExWebApp20328991.ViewModels
{
    public class FeesSearchViewModel
    {
        public virtual string PackageType { get; set; }
        [Required(ErrorMessage = "The weight field is required")]
        [RegularExpression(@"^[0-9]*|^\.*$", ErrorMessage = "Only numbers allowed")]
        [Display(Name = "weight")]
        public virtual decimal weight { get; set; }
        public virtual List<SelectListItem> PackageTypes { get; set; }
  
    }
}