using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group11_iCLOTHINGApp.Models;

namespace Group11_iCLOTHINGApp.Controllers
{
    public class aboutUsController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: ABOUT_US
        public ActionResult Index()
        {
            var aBOUT_US = db.ABOUT_US.Include(a => a.ADMINISTRATOR);
            return View(aBOUT_US.ToList());
        }

        // GET: ABOUT_US/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ABOUT_US aBOUT_US = db.ABOUT_US.Find(id);
            if (aBOUT_US == null)
            {
                return HttpNotFound();
            }
            return View(aBOUT_US);
        }

        // GET: ABOUT_US/Create
        public ActionResult Create()
        {
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName");
            return View();
        }

        // POST: ABOUT_US/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminID,companyAddress,companyShippingPolicy,companyReturnPolicy,companyContactInfo,companyBusinessDescription")] ABOUT_US aBOUT_US)
        {
            if (ModelState.IsValid)
            {
                db.ABOUT_US.Add(aBOUT_US);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName", aBOUT_US.adminID);
            return View(aBOUT_US);
        }

        // GET: ABOUT_US/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ABOUT_US aBOUT_US = db.ABOUT_US.Find(id);
            if (aBOUT_US == null)
            {
                return HttpNotFound();
            }
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName", aBOUT_US.adminID);
            return View(aBOUT_US);
        }

        // POST: ABOUT_US/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminID,companyAddress,companyShippingPolicy,companyReturnPolicy,companyContactInfo,companyBusinessDescription")] ABOUT_US aBOUT_US)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aBOUT_US).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName", aBOUT_US.adminID);
            return View(aBOUT_US);
        }

        // GET: ABOUT_US/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ABOUT_US aBOUT_US = db.ABOUT_US.Find(id);
            if (aBOUT_US == null)
            {
                return HttpNotFound();
            }
            return View(aBOUT_US);
        }

        // POST: ABOUT_US/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ABOUT_US aBOUT_US = db.ABOUT_US.Find(id);
            db.ABOUT_US.Remove(aBOUT_US);
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
