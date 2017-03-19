using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20328991.Models;
using SinExWebApp20328991.ViewModels;

namespace SinExWebApp20328991.Controllers
{
    public class ServicePackageFeesController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: ServicePackageFees

        public ActionResult Index(string currency)
        {
            var cRate = 1.0;
            if (currency == null)
            {
                ViewBag.currencyRate = 1.0;
            }
            else
            {
                cRate = db.Currencies.SingleOrDefault(s => s.CurrencyCode == currency).ExchangeRate;
                ViewBag.currencyRate = cRate;
            }
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            foreach(var item in servicePackageFees)
            {
                item.Fee = item.Fee * (decimal)cRate;
                item.MinimumFee=item.MinimumFee * (decimal)cRate;
            }
            return View(servicePackageFees.ToList());
        }
       /* public ActionResult Index()
        {
           
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            return View(servicePackageFees.ToList());
        }*/

        public ActionResult CalculateFee(string destination, string origin,int? NumberOfPackages,string ServedType,string Currency)
        {
            var serviceTypeSearch = new FeeSearchViewModel();
            serviceTypeSearch.ServiceTypes = PopulateServiceTypesDropdownList().ToList();
            serviceTypeSearch.Places = PopulatePlacesDropdownList().ToList();
            serviceTypeSearch.Currencies = PopulateCurrenciesDropdownList().ToList();
            if ((destination == null) || (origin == null) || (ServedType == null) || (NumberOfPackages == 0)||(Currency==null))
            {
               
                return View(serviceTypeSearch);
            }
            else
            {
                
                ViewBag.FeeDestination = destination;
                ViewBag.FeeOrigin = origin;
                ViewBag.NoOfPack = NumberOfPackages.ToString();
                ViewBag.ServeType = ServedType;
                var exchangeRate = db.Currencies.SingleOrDefault(s => s.CurrencyCode == Currency).ExchangeRate;
                ViewBag.exchangeRate = exchangeRate;
                this.TempData["exRate"] = Currency;
                this.TempData["FeeDestination"] = destination;
                this.TempData["FeeOrigin"] = origin;
                this.TempData["NoOfPack"] = NumberOfPackages.ToString();
                this.TempData["ServeType"] = ServedType;
                ViewBag.Switch1 = true;
                return RedirectToAction("PackagesInfo", "ServicePackageFees");
            }
        }

       public ActionResult PackagesInfo(string firstTime, string destination, string origin, string NumberOfPackage, string ServedType, string Currency, ICollection<FeesListViewModel> PackagesInformations)
        {
            var feesReportSearch = new FeesReportViewModel();
            feesReportSearch.Package = new FeesSearchViewModel();
            feesReportSearch.Packages = PackagesInformations;
            feesReportSearch.Package.PackageTypes = PopulatePackageTypesDropdownList().ToList();
            ViewBag.Switch1 = true;
            /*  if (firstTime == null)
              {

                  feesReportSearch.TotalCost = 0;
                  feesReportSearch.Packages = (IEnumerable<FeesListViewModel>)(new FeesListViewModel());
                  return View(feesReportSearch);
              }
              else if (ModelState.IsValid && firstTime != null){*/
            if (firstTime != null)
            {
                ViewBag.Switch1 = false;
            }
                var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
                var serviceFeeInfo=servicePackageFees.ToList();
                feesReportSearch.TotalCost = 0;
            if (firstTime == null)
            {
                string temp1 = this.TempData["exRate"] as string;
                string temp2 = this.TempData["FeeDestination"] as string;
                string temp3 = this.TempData["FeeOrigin"] as string;
                string temp4 = this.TempData["NoOfPack"] as string;
                string temp5 = this.TempData["ServeType"] as string;
                feesReportSearch.currency = temp1;
                feesReportSearch.destination = temp2;
                feesReportSearch.origin = temp3;
                feesReportSearch.NumberOfPackage = temp4;
                feesReportSearch.servedType = temp5;
            }
            else if(firstTime!=null && (destination != null) && (origin != null) && (ServedType != null) && (NumberOfPackage != null) && (Currency != null))
            {
                feesReportSearch.currency = Currency;
                feesReportSearch.destination = destination;
                feesReportSearch.origin = origin;
                feesReportSearch.NumberOfPackage = NumberOfPackage;
                feesReportSearch.servedType = ServedType;
            }
            ViewBag.currencyUsed = feesReportSearch.currency;
            ViewBag.FeeDestination = feesReportSearch.destination;
            ViewBag.FeeOrigin = feesReportSearch.origin;
            ViewBag.NoOfPack = feesReportSearch.NumberOfPackage;
            ViewBag.ServeType = feesReportSearch.servedType;

           
            //  decimal exRate = decimal.Parse(temp1);
            decimal exRate =  (decimal)db.Currencies.SingleOrDefault(s => s.CurrencyCode == feesReportSearch.currency).ExchangeRate;
            if (PackagesInformations!=null){
                foreach (var item in PackagesInformations)
                {
                    string itemPackageType = item.PackageType;
                    string ServiceType = ViewBag.ServeType;
                    decimal miniCost = 0;
                    decimal tempCost = 0;
                    decimal tempUnit = 0;
                    string limited = "0kg";
                    item.IsBeyond = false;

                    
                    foreach (var fee in serviceFeeInfo)
                    {
                        string temptype = (fee.PackageType.Type).ToString();
                        string tempSType = (fee.ServiceType.Type).ToString();
                        if (temptype == itemPackageType && tempSType == ServiceType)
                        {
                            miniCost = fee.MinimumFee;
                            tempUnit = fee.Fee;
                            item.PackageTypeItem = fee.PackageType;
                            item.PackageTypeItem.PackageTypeSizes = fee.PackageType.PackageTypeSizes;
                            break;
                        }
                    }
                    foreach (var templimit in item.PackageTypeItem.PackageTypeSizes)
                    {
                        if (templimit.limit == "Not Applicable")
                        {
                            limited = "Not Applicable";
                        }
                        else if (templimit.limit != "Not Applicable")
                        {
                            if (templimit.limit.CompareTo(limited) > 0)
                            {
                                limited = templimit.limit;
                            }
                        }
                    }
                    tempCost = tempUnit * (item.weight);
                    if (tempCost <= miniCost)
                    {
                        tempCost = miniCost;
                    }
                   else if (limited != "Not Applicable" && (Convert.ToDecimal(limited.Replace("kg", "")) < item.weight)&&item.PackageType!="Customer")
                    {
                        item.IsBeyond = true;
                        tempCost = tempCost + 500;
                    }
                    item.cost = (tempCost) * exRate;
                    feesReportSearch.TotalCost = feesReportSearch.TotalCost + item.cost;
                }
            }
               // return View(feesReportSearch);
           // }
            return View(feesReportSearch);
        }

        private SelectList PopulateServiceTypesDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var servicetypeQuery = db.ServiceTypes.Select(s => s.Type);
                servicetypeQuery=servicetypeQuery.Distinct().OrderBy(s => s);
            return new SelectList(servicetypeQuery);
        }

        private SelectList PopulatePlacesDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var placeQuery = db.Destinations.Select(s => s.City);
           placeQuery = placeQuery.Distinct().OrderBy(s => s);
            return new SelectList(placeQuery);
        }
        private SelectList PopulateCurrenciesDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var currencyQuery = db.Currencies.Select(s => s.CurrencyCode);
           currencyQuery = currencyQuery.Distinct().OrderBy(s => s);
            return new SelectList(currencyQuery);
        }
        private SelectList PopulatePackageTypesDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var packagetypeQuery = db.PackageTypes.Select(s => s.Type);
            packagetypeQuery = packagetypeQuery.Distinct().OrderBy(s => s);
            return new SelectList(packagetypeQuery);
        }
        // GET: ServicePackageFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Create
        public ActionResult Create()
        {
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type");
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type");
            return View();
        }

        // POST: ServicePackageFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.ServicePackageFees.Add(servicePackageFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePackageFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            db.ServicePackageFees.Remove(servicePackageFee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
