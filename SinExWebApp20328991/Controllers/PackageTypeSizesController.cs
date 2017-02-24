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
    public class PackageTypeSizesController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: PackageTypeSizes
        public ActionResult Index()
        {
            return View(db.PackageTypeSizes.ToList());
        }

        // GET: PackageTypeSizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(id);
            if (packageTypeSize == null)
            {
                return HttpNotFound();
            }
            return View(packageTypeSize);
        }

        // GET: PackageTypeSizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackageTypeSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageTypeSizeID,size,limit,PackageTypeID")] PackageTypeSize packageTypeSize)
        {
            if (ModelState.IsValid)
            {
                db.PackageTypeSizes.Add(packageTypeSize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(packageTypeSize);
        }

        // GET: PackageTypeSizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(id);
            if (packageTypeSize == null)
            {
                return HttpNotFound();
            }
            return View(packageTypeSize);
        }

        // POST: PackageTypeSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PackageTypeSizeID,size,limit,PackageTypeID")] PackageTypeSize packageTypeSize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packageTypeSize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(packageTypeSize);
        }

        // GET: PackageTypeSizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(id);
            if (packageTypeSize == null)
            {
                return HttpNotFound();
            }
            return View(packageTypeSize);
        }

        // POST: PackageTypeSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(id);
            db.PackageTypeSizes.Remove(packageTypeSize);
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
