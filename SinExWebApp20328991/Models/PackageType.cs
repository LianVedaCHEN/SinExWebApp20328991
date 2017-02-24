using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    [Table("PackageType")]
    public class PackageType
    {
        public virtual int PackageTypeID { get; set; }
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        //Navigation property to ServicePackageFee
        public virtual ICollection<ServicePackageFee> ServicePackageFee { get; set; }
        //Navigation property to PackageTypeSize
        public virtual ICollection<PackageTypeSize> PackageTypeSizes { get; set; }
    }
}