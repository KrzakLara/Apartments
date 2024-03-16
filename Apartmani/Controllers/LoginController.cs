using Apartmani.Models;
using RwaLib.Dal;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apartmani.Controllers
{
    public class LoginController : Controller
    {
        private IRepo _repo;

        public LoginController()
        {
            _repo = RepoFactory.GetRepo();
        }


        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel userLogin)
        {
            if (ModelState.IsValid)
            {
                User user = _repo.AuthUser(userLogin.UserName, Cryptography.HashPassword(userLogin.Password));

                if (user != null)
                {
                    Session["user"] = user;
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userLogin);
        }

        //odjava
        [HttpGet]
        public ActionResult Logout()
        {
            Session.RemoveAll();

            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }

    }
}