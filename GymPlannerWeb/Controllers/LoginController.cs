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
        [ValidateAntiForgeryToken]
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
                    //ModelState.AddModelError("Помилка","Неправильний логін чи пароль!");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Create()
        {
            return RedirectToAction("NewAccount", "NewAccount");
    }
    }
}