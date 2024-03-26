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
    public class ADMINISTRATORsController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: ADMINISTRATORs
        public ActionResult Index()
        {
            var aDMINISTRATOR = db.ADMINISTRATOR.Include(a => a.ABOUT_US);
            return View(aDMINISTRATOR.ToList());
        }

        // GET: ADMINISTRATORs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMINISTRATOR aDMINISTRATOR = db.ADMINISTRATOR.Find(id);
            if (aDMINISTRATOR == null)
            {
                return HttpNotFound();
            }
            return View(aDMINISTRATOR);
        }

        // GET: ADMINISTRATORs/Create
        public ActionResult Create()
        {
            ViewBag.adminID = new SelectList(db.ABOUT_US, "adminID", "companyAddress");
            return View();
        }

        // POST: ADMINISTRATORs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminID,adminName,adminEmail,dateHired")] ADMINISTRATOR aDMINISTRATOR)
        {
            if (ModelState.IsValid)
            {
                db.ADMINISTRATOR.Add(aDMINISTRATOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adminID = new SelectList(db.ABOUT_US, "adminID", "companyAddress", aDMINISTRATOR.adminID);
            return View(aDMINISTRATOR);
        }

        // GET: ADMINISTRATORs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMINISTRATOR aDMINISTRATOR = db.ADMINISTRATOR.Find(id);
            if (aDMINISTRATOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.adminID = new SelectList(db.ABOUT_US, "adminID", "companyAddress", aDMINISTRATOR.adminID);
            return View(aDMINISTRATOR);
        }

        // POST: ADMINISTRATORs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminID,adminName,adminEmail,dateHired")] ADMINISTRATOR aDMINISTRATOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDMINISTRATOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adminID = new SelectList(db.ABOUT_US, "adminID", "companyAddress", aDMINISTRATOR.adminID);
            return View(aDMINISTRATOR);
        }

        // GET: ADMINISTRATORs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMINISTRATOR aDMINISTRATOR = db.ADMINISTRATOR.Find(id);
            if (aDMINISTRATOR == null)
            {
                return HttpNotFound();
            }
            return View(aDMINISTRATOR);
        }

        // POST: ADMINISTRATORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ADMINISTRATOR aDMINISTRATOR = db.ADMINISTRATOR.Find(id);
            db.ADMINISTRATOR.Remove(aDMINISTRATOR);
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
