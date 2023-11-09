using BEShopDunk_Login_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BEShopDunk_Login_.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        DBShopDunkEntities database = new DBShopDunkEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(User _user)
        {
            var check = database.Users.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check != null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NameUser"] = _user.NameUser;
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult RegisterCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCus(Customer _cus)
        {
            if (ModelState.IsValid)
            {
                var check_phone = database.Customers.Where(s => s.CusPhone == _cus.CusPhone).FirstOrDefault();
                if (check_phone == null) 
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.Customers.Add(_cus);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "This Phone Number is exist";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }
    }
}