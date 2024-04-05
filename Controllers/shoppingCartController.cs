using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group11_iCLOTHINGApp.Models;

namespace Group11_iCLOTHINGApp.Controllers
{
    public class shoppingCartController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        // GET: SHOPPING_CART
        public ActionResult Index()
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var sHOPPING_CART = db.SHOPPING_CART.Include(s => s.CUSTOMER).Include(s => s.PRODUCT);
            return View(sHOPPING_CART.ToList());
        }

        // GET: SHOPPING_CART/Details/5
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
            SHOPPING_CART sHOPPING_CART = db.SHOPPING_CART.Find(id);
            if (sHOPPING_CART == null)
            {
                return HttpNotFound();
            }
            return View(sHOPPING_CART);
        }

        // GET: SHOPPING_CART/Create
        public ActionResult Create()
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName");
            ViewBag.productID = new SelectList(db.PRODUCT, "productID", "brandID");
            return View();
        }

        // POST: SHOPPING_CART/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartID,customerID,productID,cartProductPrice,cartProductQty")] SHOPPING_CART sHOPPING_CART)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (ModelState.IsValid)
            {
                db.SHOPPING_CART.Add(sHOPPING_CART);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", sHOPPING_CART.customerID);
            ViewBag.productID = new SelectList(db.PRODUCT, "productID", "brandID", sHOPPING_CART.productID);
            return View(sHOPPING_CART);
        }

        // GET: SHOPPING_CART/Edit/5
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
            SHOPPING_CART sHOPPING_CART = db.SHOPPING_CART.Find(id);
            if (sHOPPING_CART == null)
            {
                return HttpNotFound();
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", sHOPPING_CART.customerID);
            ViewBag.productID = new SelectList(db.PRODUCT, "productID", "brandID", sHOPPING_CART.productID);
            return View(sHOPPING_CART);
        }

        // POST: SHOPPING_CART/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartID,customerID,productID,cartProductPrice,cartProductQty")] SHOPPING_CART sHOPPING_CART)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (ModelState.IsValid)
            {
                db.Entry(sHOPPING_CART).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customerID = new SelectList(db.CUSTOMER, "customerID", "customerName", sHOPPING_CART.customerID);
            ViewBag.productID = new SelectList(db.PRODUCT, "productID", "brandID", sHOPPING_CART.productID);
            return View(sHOPPING_CART);
        }

        // GET: SHOPPING_CART/Delete/5
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
            SHOPPING_CART sHOPPING_CART = db.SHOPPING_CART.Find(id);
            if (sHOPPING_CART == null)
            {
                return HttpNotFound();
            }
            return View(sHOPPING_CART);
        }

        // POST: SHOPPING_CART/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["idUsSS"] == null || !Session["UsernameSS"].Equals("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            SHOPPING_CART sHOPPING_CART = db.SHOPPING_CART.Find(id);
            db.SHOPPING_CART.Remove(sHOPPING_CART);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerCart()
        {
            if (Session["idUsSS"] == null)
            {
                return RedirectToAction("Login", "loginRegister");
            }

            int id = int.Parse(Session["idUsSS"].ToString());

            var sHOPPING_CART = db.SHOPPING_CART.Include(s => s.CUSTOMER).Include(s => s.PRODUCT).Where(s => s.customerID.Equals(id));
            return View(sHOPPING_CART.ToList());
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
