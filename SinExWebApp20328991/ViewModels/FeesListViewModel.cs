using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinExWebApp20328991.Models;

namespace SinExWebApp20328991.ViewModels
{
    public class FeesListViewModel
    {
        public virtual string PackageType { get; set; }
        public virtual PackageType PackageTypeItem { get; set; }
        public virtual decimal weight  { get; set; }
        public virtual decimal cost { get; set; }
        public virtual bool IsBeyond { get; set; }

    }
}