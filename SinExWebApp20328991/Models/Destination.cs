using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    public class Destination
    {
        public int DestinationID { get; set; }
        public string City { get; set; }
        public string ProvinceCode {get;set;}
        //Foreign Key references CurrencyCode
        public virtual string CurrencyCode { get; set; }
        //Navigation property to Currency
        public virtual Currency Currency { get; set; }
    }
}