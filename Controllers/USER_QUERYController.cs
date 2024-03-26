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
    public class USER_QUERYController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: USER_QUERY
        public ActionResult Index()
        {
            var uSER_QUERY = db.USER_QUERY.Include(u => u.CUSTOMER);
            return View(uSER_QUERY.ToList());
        }

        // GET: USER_QUERY/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_QUERY uSER_QUERY = db.USER_QUERY.Find(id);
            if (uSER_QUERY == null)
            {
                return HttpNotFound();
            }
            return View(uSER_QUERY);
        }

        // GET: USER_QUERY/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName");
            return View();
        }

        // POST: USER_QUERY/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "queryNo,customerID,queryDate,queryDescription")] USER_QUERY uSER_QUERY)
        {
            if (ModelState.IsValid)
            {
                db.USER_QUERY.Add(uSER_QUERY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_QUERY.customerID);
            return View(uSER_QUERY);
        }

        // GET: USER_QUERY/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_QUERY uSER_QUERY = db.USER_QUERY.Find(id);
            if (uSER_QUERY == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_QUERY.customerID);
            return View(uSER_QUERY);
        }

        // POST: USER_QUERY/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "queryNo,customerID,queryDate,queryDescription")] USER_QUERY uSER_QUERY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_QUERY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_QUERY.customerID);
            return View(uSER_QUERY);
        }

        // GET: USER_QUERY/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_QUERY uSER_QUERY = db.USER_QUERY.Find(id);
            if (uSER_QUERY == null)
            {
                return HttpNotFound();
            }
            return View(uSER_QUERY);
        }

        // POST: USER_QUERY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            USER_QUERY uSER_QUERY = db.USER_QUERY.Find(id);
            db.USER_QUERY.Remove(uSER_QUERY);
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
