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

        public ActionResult NewAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Users U)
        {
            try
            {
                if (UsersExists(U.Login))
                {
                    //ViewBag.Message = "Користувач з таким логіном вже існує!";
                    return View(U);
                }
                if (ModelState.IsValid)
                    db.Users.Add(U);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return View(U);
            }
        }
        public ActionResult Back()
        {
            return RedirectToAction("Login", "Login");
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