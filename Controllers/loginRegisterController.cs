using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Group11_iCLOTHINGApp.Models;
using BCrypt;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Data.SqlClient;



namespace Group11_iCLOTHINGApp.Controllers
{
    public class loginRegisterController : Controller
    {
        Group11_iCLOTHINGDBEntities db = new Group11_iCLOTHINGDBEntities();
        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(USER_PASSWORD uSER_PASSWORD)
        {
            if(db.USER_PASSWORD.Any(x=>x.userAccountName == uSER_PASSWORD.userAccountName))
            {
                ViewBag.Notification = "This account already exists.";
                return View();
            } else
            {
                if(uSER_PASSWORD.userEncryptedPassword == uSER_PASSWORD.reEncryptedPassword)
                {
                    uSER_PASSWORD.userEncryptedPassword = BCrypt.Net.BCrypt.HashPassword(uSER_PASSWORD.userEncryptedPassword.ToString()).ToString();
                    uSER_PASSWORD.reEncryptedPassword = uSER_PASSWORD.userEncryptedPassword;
                }

                DateTime currentDate = DateTime.Now;
                uSER_PASSWORD.passwordExpiryTime = currentDate.AddDays(90);
                uSER_PASSWORD.userAccountExpiryDate = currentDate.AddDays(1000);
                uSER_PASSWORD.userID = db.USER_PASSWORD.Count() + 1;
                db.USER_PASSWORD.Add(uSER_PASSWORD);
                
                // This try - catch is designed just to give more verbose error messages to db connection errors
                try
                {
                    db.SaveChanges();
                } catch(System.Data.Entity.Validation.DbEntityValidationException e){
                    Exception raise = e;
                    foreach(var validationErrs in e.EntityValidationErrors)
                    {
                        foreach(var validationErr in validationErrs.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}:{2}:{3}", validationErrs.Entry.Entity.ToString(),
                                validationErr.ErrorMessage, uSER_PASSWORD.userEncryptedPassword.Length, uSER_PASSWORD.userEncryptedPassword);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                Session["idUsSS"] = uSER_PASSWORD.userID.ToString();
                Session["UsernameSS"] = uSER_PASSWORD.userAccountName.ToString();
                return RedirectToAction("Index", "Home");

            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USER_PASSWORD uSER_PASSWORD)
        {
            string enteredUsername = uSER_PASSWORD.userAccountName;
            string enteredPassword = uSER_PASSWORD.userEncryptedPassword;

            var selectQueryString = "SELECT userEncryptedPassword " +
                        "FROM USER_PASSWORD " +
                        "WHERE userAccountName = @Username";

            string encryptedPassword = db.Database
                .SqlQuery<string>(selectQueryString, new SqlParameter("@Username", enteredUsername))
                .FirstOrDefault();

            var checkLogin = false;
            if (encryptedPassword != null)
            {
                checkLogin = BCrypt.Net.BCrypt.Verify(enteredPassword, encryptedPassword, false, BCrypt.Net.HashType.SHA384);
            }

            if (checkLogin) 
            {
                var selectIdString = "SELECT userID " +
                                        "FROM USER_PASSWORD " +
                                        "WHERE userAccountName = @Username";

                int id = db.Database
                    .SqlQuery<int>(selectIdString, new SqlParameter("@Username", enteredUsername))
                    .FirstOrDefault();

                Session["idUsSS"] = id;
                Session["UsernameSS"] = uSER_PASSWORD.userAccountName.ToString();
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }
    }
}