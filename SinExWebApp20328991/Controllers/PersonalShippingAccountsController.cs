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
    public class PersonalShippingAccountsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

      

        // GET: PersonalShippingAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalShippingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingAccountId,Email,PhoneNumber,Building,Street,City,Province,PostalCode,CardType,CardNumber,SecurityNumber,CardholderName,ExpiryMonth,ExpiryYear,FirstName,LastName")] PersonalShippingAccount personalShippingAccount)
        {
            if (ModelState.IsValid)
            {
                db.ShippingAccount.Add(personalShippingAccount);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(personalShippingAccount);
        }

        // GET: PersonalShippingAccounts/Edit/5
        public ActionResult Edit()
        {
            if (System.Web.HttpContext.Current.User.IsInRole("Customer"))
            {

                string tempUserName = System.Web.HttpContext.Current.User.Identity.Name;
                var tempShippingAccount = db.ShippingAccount.SingleOrDefault(s => s.UserName == tempUserName);
                int tempId = tempShippingAccount.ShippingAccountId;
                PersonalShippingAccount personalShippingAccount = (PersonalShippingAccount)db.ShippingAccount.Find(tempId);
                return View(personalShippingAccount);
            }
            if (System.Web.HttpContext.Current.User.IsInRole("Employee"))
            {

              
                return View("There is no infomration about Employee Account so you can not edit! ");
            }

            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalShippingAccount personalShippingAccount = (PersonalShippingAccount)db.ShippingAccount.Find(id);
            if (personalShippingAccount == null)
            {
                return HttpNotFound();
            }*/
            return HttpNotFound();
        }

        // POST: PersonalShippingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingAccountId,Email,PhoneNumber,Building,Street,City,Province,PostalCode,CardType,CardNumber,SecurityNumber,CardholderName,ExpiryMonth,ExpiryYear,FirstName,LastName")] PersonalShippingAccount personalShippingAccount)
        {
            if (ModelState.IsValid)
            {
                personalShippingAccount.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                db.Entry(personalShippingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(personalShippingAccount);
        }

     

        // POST: PersonalShippingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalShippingAccount personalShippingAccount = (PersonalShippingAccount)db.ShippingAccount.Find(id);
            db.ShippingAccount.Remove(personalShippingAccount);
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
