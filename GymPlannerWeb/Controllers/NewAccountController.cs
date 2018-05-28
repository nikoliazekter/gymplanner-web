using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http.Description;
using GymPlannerWeb;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class NewAccountController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        [HttpGet]
        public ActionResult NewAccount(Users user)
        {
            return View(user);
        }

        [HttpPost]
        [ActionName("NewAccount")]
        public ActionResult CreateUser(Users user)
        {
            try
            {
                if (UsersExists(user.Login))
                {
                    ModelState.AddModelError("", "Користувач з таким логіном вже існує!");
                    return View(user);
                }
                if (ModelState.IsValid)
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return View(user);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(string login)
        {
            return db.Users.Count(e => e.Login == login) > 0;
        }
    }
}