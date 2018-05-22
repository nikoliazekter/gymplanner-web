using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class LoginController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Users user)
        {
            if (ModelState.IsValid)
            {
                var user2 = (from u in db.Users where (u.Login == user.Login && u.Password == user.Password) select u).FirstOrDefault();
                if (user2 != null)
                {
                    return RedirectToAction("CalendarRender", "Calendar", new { user.Login });
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Create()
        {
            Users U = new Users();
            ViewBag.CmpList = db.Users.ToList();
            return View(U);
        }

        [HttpPost]
        public ActionResult CreateU(Users U)
        {
            try
            {
                if (ModelState.IsValid)
                    db.Users.Add(U);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return View(U);
            }
        }
    }
}