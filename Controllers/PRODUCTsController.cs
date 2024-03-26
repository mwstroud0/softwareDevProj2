﻿using System;
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
    public class PRODUCTsController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: PRODUCTs
        public ActionResult Index()
        {
            var pRODUCT = db.PRODUCT.Include(p => p.ADMINISTRATOR).Include(p => p.BRAND).Include(p => p.CATEGORY).Include(p => p.DEPARTMENT);
            return View(pRODUCT.ToList());
        }

        // GET: PRODUCTs/Details/5
        public ActionResult Details(string id)
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

        // GET: PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.adminID = new SelectList(db.ADMINISTRATOR, "adminID", "adminName");
            ViewBag.brandID = new SelectList(db.BRAND, "brandID", "brandName");
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "departmentID");
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName");
            return View();
        }

        // POST: PRODUCTs/Create
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
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "departmentID", pRODUCT.categoryID);
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName", pRODUCT.departmentID);
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "departmentID", pRODUCT.categoryID);
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName", pRODUCT.departmentID);
            return View(pRODUCT);
        }

        // POST: PRODUCTs/Edit/5
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
            ViewBag.categoryID = new SelectList(db.CATEGORY, "categoryID", "departmentID", pRODUCT.categoryID);
            ViewBag.departmentID = new SelectList(db.DEPARTMENT, "departmentID", "departmentName", pRODUCT.departmentID);
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Delete/5
        public ActionResult Delete(string id)
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

        // POST: PRODUCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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
