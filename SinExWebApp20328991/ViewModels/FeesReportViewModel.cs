using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinExWebApp20328991.Models;

namespace SinExWebApp20328991.ViewModels
{
    public class FeesReportViewModel
    {
        public virtual FeesSearchViewModel Package { get; set; }
        public virtual ICollection<FeesListViewModel> Packages { get; set; }
        public virtual string currency { get; set; }
        public virtual string origin { get; set; }
        public virtual string destination { get; set; }
        public virtual string NumberOfPackage { get; set; }
        public virtual string servedType { get; set; }
        public virtual decimal TotalCost { get; set; }

    }
}