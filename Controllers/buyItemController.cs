using Group11_iCLOTHINGApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Group11_iCLOTHINGApp.Controllers
{
    public class buyItemController : Controller
    {
        private Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();

        public ActionResult BuyItem()
        {
            //get cardID
            int customerID = int.Parse(Session["idUsSS"].ToString());
            SHOPPING_CART cart = db.SHOPPING_CART.Find(customerID);

            //update order status to confirmed
            RedirectToAction("UpdateStatus", "shoppingCart", new { status = "Confirmed", cartID = cart.cartID });

            //have customer enter payment information
            RedirectToAction("EnterInfo", "shoppingCart");


            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}