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

        public ActionResult CustomerCart()
        {
            ViewBag.notification = TempData["shortMessage"];    
            var itemsList = new List<ITEM>();
            int customerID = int.Parse(Session["idUsSS"].ToString());

            SHOPPING_CART cART = db.SHOPPING_CART.Where(c => c.customerID == customerID).FirstOrDefault();

            if(cART == null)
            {
                itemsList = new List<ITEM>();
                Session["cart"] = itemsList;
            }
            else
            {
                //note: we don't loop here since we do not have the functionality to support adding multiple products to the cart
                //  we use a List<ITEM> rather than ITEM to continue support for previously written code

                PRODUCT product = db.PRODUCT.Where(p => p.productID == cART.productID).FirstOrDefault();
                ITEM item = new ITEM();
                item.productID = product.productID;
                item.brandID = product.brandID;
                item.categoryID = product.categoryID;
                item.adminID = product.adminID;
                item.departmentID = product.departmentID;
                item.productName = product.productName;
                item.productDescription = product.productDescription;
                item.productPrice = product.productPrice;
                item.itemQty = (int)cART.cartProductQty;
                item.productQty = product.productQty;

                itemsList.Add(item);
            }
            var model = new Tuple<IEnumerable<ITEM>, SHOPPING_CART>(itemsList, cART);
            return View("~/Views/ShoppingCart/CustomerCart.cshtml", model);
        }

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

        public ActionResult AddToCart(int? id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }

            List<ITEM> itemList;
            // If the cart is empty, add a new item to the cart
            if (Session["cart"] == null)
            {
                itemList = new List<ITEM>(); 

                ITEM newItem = new ITEM();

                newItem.itemID = pRODUCT.productID;
                newItem.productName = pRODUCT.productName;
                newItem.productID = pRODUCT.productID;
                newItem.departmentID = pRODUCT.departmentID;
                newItem.adminID = pRODUCT.adminID;
                newItem.categoryID = pRODUCT.categoryID;
                newItem.brandID = pRODUCT.brandID;
                newItem.productDescription = pRODUCT.productDescription;
                newItem.productPrice = pRODUCT.productPrice;
                newItem.productQty = pRODUCT.productQty;
                newItem.itemQty = 1; // Just added 1 to the count

                itemList.Add(newItem);
                Session["cartCount"] = 1;
            }
            else
            {
                // Search cart for the same itemID
                itemList = (List<ITEM>)Session["cart"];
                foreach(ITEM item in itemList)
                {
                    if(item.productID == pRODUCT.productID)
                    {
                        item.itemQty++;
                        Session["cart"] = itemList;
                        return RedirectToAction("UpdateCart", "shoppingCart");
                    }
                }

                //add new item to the cart
                ITEM newItem = new ITEM();
                newItem.itemID = pRODUCT.productID;
                newItem.productName = pRODUCT.productName;
                newItem.productID = pRODUCT.productID;
                newItem.departmentID = pRODUCT.departmentID;
                newItem.adminID = pRODUCT.adminID;
                newItem.categoryID = pRODUCT.categoryID;
                newItem.brandID = pRODUCT.brandID;
                newItem.productDescription = pRODUCT.productDescription;
                newItem.productPrice = pRODUCT.productPrice;
                newItem.productQty = pRODUCT.productQty;
                newItem.itemQty = 1; // Just added 1 to the count

                itemList.Add(newItem);
            }

            Session["cart"] = itemList;
            Session["cartCount"] = 1;

            return RedirectToAction("UpdateCart", "shoppingCart");
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
