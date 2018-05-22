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
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Console.Out.WriteLine(error.ErrorMessage);
                }
            }
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
    }
}