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
    public class customersController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: CUSTOMERs
        public ActionResult Index()
        {
            var cUSTOMER = db.CUSTOMER.Include(c => c.USER_PASSWORD);
            return View(cUSTOMER.ToList());
        }

        // GET: CUSTOMERs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMER.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.USER_PASSWORD, "userID", "userAccountName");
            return View();
        }

        // POST: CUSTOMERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerID,customerName,customerShippingAddress,customerBillingAddress,customerDOB,customerGender")] CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMER.Add(cUSTOMER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.USER_PASSWORD, "userID", "userAccountName", cUSTOMER.customerID);
            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMER.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.USER_PASSWORD, "userID", "userAccountName", cUSTOMER.customerID);
            return View(cUSTOMER);
        }

        // POST: CUSTOMERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerID,customerName,customerShippingAddress,customerBillingAddress,customerDOB,customerGender")] CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUSTOMER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.USER_PASSWORD, "userID", "userAccountName", cUSTOMER.customerID);
            return View(cUSTOMER);
        }

        // GET: CUSTOMERs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMER.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }

        // POST: CUSTOMERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CUSTOMER cUSTOMER = db.CUSTOMER.Find(id);
            db.CUSTOMER.Remove(cUSTOMER);
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
