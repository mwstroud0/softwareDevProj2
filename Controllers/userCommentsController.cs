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
    public class userCommentsController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: USER_COMMENTS
        public ActionResult Index()
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var uSER_COMMENTS = db.USER_COMMENTS.Include(u => u.CUSTOMER);
            return View(uSER_COMMENTS.ToList());
        }

        // GET: USER_COMMENTS/Details/5
        public ActionResult Details(int id)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_COMMENTS uSER_COMMENTS = db.USER_COMMENTS.Find(id);
            if (uSER_COMMENTS == null)
            {
                return HttpNotFound();
            }
            return View(uSER_COMMENTS);
        }

        // GET: USER_COMMENTS/Create
        public ActionResult Create()
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName");
            return View();
        }

        // POST: USER_COMMENTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentNo,customerID,commentDate,commentDescription")] USER_COMMENTS uSER_COMMENTS)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (ModelState.IsValid)
            {
                db.USER_COMMENTS.Add(uSER_COMMENTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_COMMENTS.customerID);
            return View(uSER_COMMENTS);
        }

        // GET: USER_COMMENTS/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_COMMENTS uSER_COMMENTS = db.USER_COMMENTS.Find(id);
            if (uSER_COMMENTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_COMMENTS.customerID);
            return View(uSER_COMMENTS);
        }

        // POST: USER_COMMENTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentNo,customerID,commentDate,commentDescription")] USER_COMMENTS uSER_COMMENTS)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (ModelState.IsValid)
            {
                db.Entry(uSER_COMMENTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", uSER_COMMENTS.customerID);
            return View(uSER_COMMENTS);
        }

        // GET: USER_COMMENTS/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_COMMENTS uSER_COMMENTS = db.USER_COMMENTS.Find(id);
            if (uSER_COMMENTS == null)
            {
                return HttpNotFound();
            }
            return View(uSER_COMMENTS);
        }

        // POST: USER_COMMENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            USER_COMMENTS uSER_COMMENTS = db.USER_COMMENTS.Find(id);
            db.USER_COMMENTS.Remove(uSER_COMMENTS);
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
