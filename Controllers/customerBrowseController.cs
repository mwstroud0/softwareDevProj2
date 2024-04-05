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
    public class customerBrowseController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: customerBrowse
        public ActionResult Index(int? departmentID, int? categoryID)
        {
            if (departmentID == null && categoryID == null)
            {
                var pRODUCT = db.PRODUCT.Include(p => p.ADMINISTRATOR).Include(p => p.BRAND).Include(p => p.CATEGORY).Include(p => p.DEPARTMENT);
                return View(pRODUCT.ToList());
            }
            else if (departmentID != null && categoryID == null)
            {
                var pRODUCT = db.PRODUCT.AsQueryable();
                pRODUCT = pRODUCT.Where(p => p.departmentID == departmentID);
                return View(pRODUCT.ToList());
            }
            else if (departmentID == null && categoryID != null)
            {
                var pRODUCT = db.PRODUCT.AsQueryable();
                pRODUCT = pRODUCT.Where(p => p.categoryID == categoryID);
                return View(pRODUCT.ToList());
            }
            else
            {
                var pRODUCT = db.PRODUCT.AsQueryable();
                pRODUCT = pRODUCT.Where(p => p.categoryID == categoryID);
                pRODUCT = pRODUCT.Where(p => p.departmentID == departmentID);
                return View(pRODUCT.ToList());

            }
        }

        public ActionResult Search(string search)
        {
            var pRODUCT = db.PRODUCT.AsQueryable();
            pRODUCT = pRODUCT.Where(p => p.productDescription.Contains(search) || p.productName.Contains(search));
            return View(pRODUCT.ToList());
        }

        // GET: customerBrowse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: customerBrowse/Create
        public ActionResult Create()
        {
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName");
            ViewBag.brandID = new SelectList(db.BRAND, "brandID", "brandName");
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "categoryName");
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName");
            return View();
        }

        // POST: customerBrowse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productID,brandID,categoryID,adminID,departmentID,productName,productDescription,productPrice,productQty")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCT.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName", pRODUCT.adminID);
            ViewBag.brandID = new SelectList(db.BRAND, "brandID", "brandName", pRODUCT.brandID);
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "categoryName", pRODUCT.categoryID);
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName", pRODUCT.departmentID);
            return View(pRODUCT);
        }

        // GET: customerBrowse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName", pRODUCT.adminID);
            ViewBag.brandID = new SelectList(db.BRAND, "brandID", "brandName", pRODUCT.brandID);
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "categoryName", pRODUCT.categoryID);
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName", pRODUCT.departmentID);
            return View(pRODUCT);
        }

        // POST: customerBrowse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,brandID,categoryID,adminID,departmentID,productName,productDescription,productPrice,productQty")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName", pRODUCT.adminID);
            ViewBag.brandID = new SelectList(db.BRAND, "brandID", "brandName", pRODUCT.brandID);
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "categoryName", pRODUCT.categoryID);
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName", pRODUCT.departmentID);
            return View(pRODUCT);
        }

        // GET: customerBrowse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: customerBrowse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            db.PRODUCT.Remove(pRODUCT);
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
