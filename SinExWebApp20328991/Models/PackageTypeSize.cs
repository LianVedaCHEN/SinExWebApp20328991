using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SinExWebApp20328991.Models
{
    [Table("PackageTypeSize")]
    public class PackageTypeSize
    {
        public virtual int PackageTypeSizeID { get; set; }
        public virtual string size { get; set; }
        public virtual string limit { get; set; }
        public virtual int PackageTypeID { get; set; }
        public virtual PackageType PackageType { get; set; }

    }
}