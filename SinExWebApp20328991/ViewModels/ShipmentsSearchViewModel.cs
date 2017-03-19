using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.ViewModels
{
    public class ShipmentsSearchViewModel
    {
        public virtual int ShippingAccountId { get; set; }
    
        [DataType(DataType.DateTime)]
        public virtual DateTime EndDate { get; set; }
       
        [DataType(DataType.DateTime)]
        public virtual DateTime StartDate { get; set; }
        public virtual List<SelectListItem> ShippingAccounts { get; set; }
    }
}