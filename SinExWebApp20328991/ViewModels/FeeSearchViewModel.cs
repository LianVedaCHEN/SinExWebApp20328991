using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace SinExWebApp20328991.ViewModels
{
    public class FeeSearchViewModel
    {
        public virtual string destination { get; set; }
        public virtual string origin { get; set; }
        [Required(ErrorMessage = "The number of package field is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers allowed")]
        public virtual int NumberOfPackages { get; set; }
        public virtual string ServedType { get; set; }
        public virtual string Currency { get; set; }

        public virtual List<SelectListItem> ServiceTypes { get; set; }
        public virtual List<SelectListItem> Places { get; set; }
        public virtual List<SelectListItem> Currencies { get; set; }
    }
}