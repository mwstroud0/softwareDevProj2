using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Group11_iCLOTHINGApp.Models;

namespace Group11_iCLOTHINGApp.Controllers
{
    public class paymentInfoController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: paymentInfo
        public ActionResult Index()
        {
            var cUSTOMER = db.CUSTOMER.Include(c => c.USER_PASSWORD);
            return View(cUSTOMER.ToList());
        }
        public ActionResult paymentSuccess()
        {
            var shoppingCart = (List<ITEM>)Session["cart"];
            int customerID = int.Parse(Session["idUsSS"].ToString());
    
            var iTEM_DELIVERY = new ITEM_DELIVERY();
            iTEM_DELIVERY.customerID = customerID;
            iTEM_DELIVERY.stickerDate = DateTime.Now;
            iTEM_DELIVERY.stickerID = db.ITEM_DELIVERY.Count() + 2;
            iTEM_DELIVERY.productID = shoppingCart[0].productID;

            if (ModelState.IsValid) {
                db.ITEM_DELIVERY.Add(iTEM_DELIVERY);
                db.SaveChanges();
                return RedirectToAction("UpdateProductQuantity", "products", new { productID = iTEM_DELIVERY.productID });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Submit(FormCollection formCollection)
        {
            String name = formCollection["name"];
            String cardNumber = formCollection["cardnumber"];
            String expiryDate = formCollection["expiry"];
            String cvv = formCollection["cvv"];
            String streetAddress = formCollection["saddress"];
            String billingAddress = formCollection["baddress"];

            return RedirectToAction("paymentSuccess");
        }

        // GET: paymentInfo/Details/5
        public ActionResult Details(int? id)
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

        // GET: paymentInfo/Create
        public ActionResult Create()
        {
            ViewBag.customerID = new SelectList(db.USER_PASSWORD, "userID", "userAccountName");
            return View();
        }

        // POST: paymentInfo/Create
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

        // GET: paymentInfo/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: paymentInfo/Edit/5
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

        // GET: paymentInfo/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: paymentInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
