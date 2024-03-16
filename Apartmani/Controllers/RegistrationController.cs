using RwaLib.Dal;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apartmani.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration

        private IRepo _repo;

        public RegistrationController()
        {
            _repo = RepoFactory.GetRepo();
        }

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
        public ActionResult Index(User user)
        {
            if(ModelState.IsValid)
            {
                _repo.CreateNewUser(user);

                Session["user"] = user;

                return RedirectToAction("Index", "Home");
            }

            return Index();
        }
}
}