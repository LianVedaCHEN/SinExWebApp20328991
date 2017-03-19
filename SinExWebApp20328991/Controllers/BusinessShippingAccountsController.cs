using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20328991.Models;

namespace SinExWebApp20328991.Controllers
{
    public class BusinessShippingAccountsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

       

        // GET: BusinessShippingAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessShippingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingAccountId,Email,PhoneNumber,Building,Street,City,Province,PostalCode,CardType,CardNumber,SecurityNumber,CardholderName,ExpiryMonth,ExpiryYear,ContactPersonName,CompanyName,DepartmentName")] BusinessShippingAccount businessShippingAccount)
        {
            if (ModelState.IsValid)
            {
                //db.ShippingAccount.Add(businessShippingAccount);
                //db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(businessShippingAccount);
        }

        // GET: BusinessShippingAccounts/Edit/5
        public ActionResult Edit()
        {
            if (System.Web.HttpContext.Current.User.IsInRole("Customer"))
            {

                string tempUserName = System.Web.HttpContext.Current.User.Identity.Name;
                var tempShippingAccount = db.ShippingAccount.SingleOrDefault(s => s.UserName == tempUserName);
                int tempId = tempShippingAccount.ShippingAccountId;
                BusinessShippingAccount BusinessShippingAccount = (BusinessShippingAccount)db.ShippingAccount.Find(tempId);
                return View(BusinessShippingAccount);
            }
            if (System.Web.HttpContext.Current.User.IsInRole("Employee"))
            {


                return View("There is no infomration about Employee Account so you can not edit! ");
            }

            
            return HttpNotFound();
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessShippingAccount businessShippingAccount = (BusinessShippingAccount)db.ShippingAccount.Find(id);
            if (businessShippingAccount == null)
            {
                return HttpNotFound();
            }
            return View(businessShippingAccount);*/
        }

        // POST: BusinessShippingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingAccountId,Email,PhoneNumber,Building,Street,City,Province,PostalCode,CardType,CardNumber,SecurityNumber,CardholderName,ExpiryMonth,ExpiryYear,ContactPersonName,CompanyName,DepartmentName")] BusinessShippingAccount businessShippingAccount)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(businessShippingAccount).State = EntityState.Modified;
               // db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(businessShippingAccount);
        }


        // POST: BusinessShippingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessShippingAccount businessShippingAccount = (BusinessShippingAccount)db.ShippingAccount.Find(id);
            db.ShippingAccount.Remove(businessShippingAccount);
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
