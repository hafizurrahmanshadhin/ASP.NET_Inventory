using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account Model)
        {
            if(String.IsNullOrEmpty(Model.Username) || String.IsNullOrEmpty(Model.Password))
            {
                ViewBag.Message = "Please enter username and password";
            }

            if (Model.Username == "user" && Model.Password == "12345678")
            {
                Session["User"] = Model.Username;
                ViewBag.Message = "Login Successful";
            }
            else
            {
                ViewBag.Message = "Login Failed";
            }
            return View();
        }
    }
}

//Next Module 7 Razor View Engine after 48 minutes