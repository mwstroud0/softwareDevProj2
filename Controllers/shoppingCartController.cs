using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public void UpdateStatus(string status, int cartID)
        {
            //create new ORDER_STATUS object and fill in values
            ORDER_STATUS orderStatus = new ORDER_STATUS();
            orderStatus.statusID = db.ORDER_STATUS.Count() + 1;
            orderStatus.cartID = cartID;
            orderStatus.orderStatus = status;
            orderStatus.statusDate = DateTime.Now;

            //store the ORDER_STATUS object in the database          
            db.ORDER_STATUS.Add(orderStatus);
            db.SaveChanges();
        }

        public ActionResult EnterInfo()
        {
            return View();
        }

        // call this method after modifying the cart to update/store the current shopping cart
        public ActionResult UpdateCart()
        {
            // currently, cart is stored only in Session["cart"]
            // need to store the cart in the database

            List<ITEM> itemsList = (List<ITEM>)Session["cart"];

            if(itemsList == null || itemsList.Count == 0)
            {
                //session cart is empty, nothing to update
                return RedirectToAction("Index", "customerBrowse");
            }

            //create the SHOPPING_CART object, and fill values
            // note: only functional to add one item
            SHOPPING_CART cART = new SHOPPING_CART();
            cART.cartID = db.SHOPPING_CART.Count() + 1;
            cART.customerID = int.Parse(Session["idUsSS"].ToString());
            cART.productID = itemsList[0].productID;
            cART.cartProductQty = itemsList[0].itemQty;
            cART.cartProductPrice = (int)itemsList[0].productPrice;

            //add the object to the db
            if (ModelState.IsValid)
            {
                db.SHOPPING_CART.Add(cART);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    Exception raise = e;
                    foreach (var validationErrs in e.EntityValidationErrors)
                    {
                        foreach (var validationErr in validationErrs.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}:{2}:{3}", validationErrs.Entry.Entity.ToString(),
                                validationErr.ErrorMessage, cART.cartID, cART.productID);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }

            return RedirectToAction("CustomerCart", "customerBrowse");
        }

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

        public ActionResult CustomerCart()
        {
            if (Session["idUsSS"] == null)
            {
                return RedirectToAction("Login", "loginRegister");
            }
            SHOPPING_CART sHOPPING = db.SHOPPING_CART.First();

            if(sHOPPING == null)
            {
                Session["cartCount"] = 0;
            } else
            {
                Session["cartCount"] = sHOPPING.cartProductQty;
            }

            int id = int.Parse(Session["idUsSS"].ToString());

            var sHOPPING_CART = db.SHOPPING_CART.Include(s => s.CUSTOMER).Include(s => s.PRODUCT).Where(s => s.customerID.Equals(id));
            return View(sHOPPING_CART.ToList());
        }

        public ActionResult Increment()
        {
            SHOPPING_CART sHOPPING = db.SHOPPING_CART.First();
            PRODUCT pRODUCT = db.PRODUCT.Find(sHOPPING.productID);

            sHOPPING.cartProductQty++;
            sHOPPING.cartProductPrice = sHOPPING.cartProductQty * (int) pRODUCT.productPrice;
            Session["cartCount"] = sHOPPING.cartProductQty;

            if (ModelState.IsValid)
            {
                db.Entry(sHOPPING).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    Exception raise = e;
                    foreach (var validationErrs in e.EntityValidationErrors)
                    {
                        foreach (var validationErr in validationErrs.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}:{2}:{3}", validationErrs.Entry.Entity.ToString(),
                                validationErr.ErrorMessage, sHOPPING.cartID, sHOPPING.productID);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return RedirectToAction("CustomerCart", "customerBrowse");
        }

        public ActionResult Decrement()
        {
            
            SHOPPING_CART sHOPPING = db.SHOPPING_CART.First();
            PRODUCT pRODUCT = db.PRODUCT.Find(sHOPPING.productID);

            sHOPPING.cartProductQty--;
            sHOPPING.cartProductPrice = sHOPPING.cartProductQty * (int) pRODUCT.productPrice;
            Session["cartCount"] = sHOPPING.cartProductQty;

            if (sHOPPING.cartProductQty <= 0)
            {
                db.SHOPPING_CART.Remove(sHOPPING);
                db.SaveChanges();
                return RedirectToAction("CustomerCart", "customerBrowse");
            }

            if (ModelState.IsValid)
            {
                db.Entry(sHOPPING).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    Exception raise = e;
                    foreach (var validationErrs in e.EntityValidationErrors)
                    {
                        foreach (var validationErr in validationErrs.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}:{2}:{3}", validationErrs.Entry.Entity.ToString(),
                                validationErr.ErrorMessage, sHOPPING.cartID, sHOPPING.productID);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return RedirectToAction("CustomerCart", "customerBrowse");
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
