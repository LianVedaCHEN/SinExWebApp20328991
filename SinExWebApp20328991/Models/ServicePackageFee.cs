using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    [Table("ServicePackageFee")]
    public class ServicePackageFee
    {
        public virtual int ServicePackageFeeID { get; set; }
        public virtual decimal Fee { get; set; }
        public virtual decimal MinimumFee { get; set; }
        //Foreign Key references PackageType
        public virtual int PackageTypeID { get; set; }
        //Foreign Key references ServiceType
        public virtual int ServiceTypeID { get; set; }
        //Navigation property to PackageType
        public virtual PackageType PackageType{get; set;}
        //Navigation property to ServiceType
        public virtual ServiceType ServiceType { get; set; }



    }
}