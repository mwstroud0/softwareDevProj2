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
    public class USER_PASSWORDController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: USER_PASSWORD
        public ActionResult Index()
        {
            var uSER_PASSWORD = db.USER_PASSWORD.Include(u => u.CUSTOMER);
            return View(uSER_PASSWORD.ToList());
        }

        // GET: USER_PASSWORD/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_PASSWORD uSER_PASSWORD = db.USER_PASSWORD.Find(id);
            if (uSER_PASSWORD == null)
            {
                return HttpNotFound();
            }
            return View(uSER_PASSWORD);
        }

        // GET: USER_PASSWORD/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.CUSTOMER, "customerID", "customerName");
            return View();
        }

        // POST: USER_PASSWORD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,userAccountName,userEncryptedPassword,passwordExpiryTime,userAccountExpiryDate")] USER_PASSWORD uSER_PASSWORD)
        {
            if (ModelState.IsValid)
            {
                db.USER_PASSWORD.Add(uSER_PASSWORD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_PASSWORD.userID);
            return View(uSER_PASSWORD);
        }

        // GET: USER_PASSWORD/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_PASSWORD uSER_PASSWORD = db.USER_PASSWORD.Find(id);
            if (uSER_PASSWORD == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_PASSWORD.userID);
            return View(uSER_PASSWORD);
        }

        // POST: USER_PASSWORD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userAccountName,userEncryptedPassword,passwordExpiryTime,userAccountExpiryDate")] USER_PASSWORD uSER_PASSWORD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_PASSWORD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_PASSWORD.userID);
            return View(uSER_PASSWORD);
        }

        // GET: USER_PASSWORD/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_PASSWORD uSER_PASSWORD = db.USER_PASSWORD.Find(id);
            if (uSER_PASSWORD == null)
            {
                return HttpNotFound();
            }
            return View(uSER_PASSWORD);
        }

        // POST: USER_PASSWORD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            USER_PASSWORD uSER_PASSWORD = db.USER_PASSWORD.Find(id);
            db.USER_PASSWORD.Remove(uSER_PASSWORD);
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
