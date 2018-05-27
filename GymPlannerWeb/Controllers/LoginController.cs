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

        [HttpGet]
        public ActionResult Login(Users user)
        {
            return View(user);
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult SignIn(Users user)
        {
            if (ModelState.IsValid)
            {
                var user2 = (from u in db.Users where (u.Login == user.Login && u.Password == user.Password) select u).FirstOrDefault();
                if (user2 != null)
                {
                    return RedirectToAction("CalendarRender", "Calendar", new { user.Login });
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін чи пароль!");
                }
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return RedirectToAction("NewAccount", "NewAccount");
        }
    }
}