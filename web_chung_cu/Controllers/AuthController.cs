using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using web_chung_cu.Models;
using web_chung_cu.Models.Services;

namespace web_chung_cu.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService = new UserService();

        //GET: Register
        public ActionResult Register()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                User user = _userService.GetOneByEmail(_user.email);
                if (user == null)
                {
                    _userService.AddOne(_user);

                    //add session
                    Session["user"] = _user;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError(_user.email, "Email đã tồn tại!");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }

            User user = _userService.GetOneByEmailAndPassword(email, password);
            if (user != null)
            {
                //add session
                Session["user"] = user;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("email", "Email hoặc mật khẩu không chính xác!");
                ModelState.AddModelError("password", "Email hoặc mật khẩu không chính xác!");
            }

            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login", "Auth");
        }
    }
}