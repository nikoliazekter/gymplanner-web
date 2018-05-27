using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class CalendarController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Calendar(Users user)
        {
            Session["Login"] = user.Login;
            return View((from u in db.Users where u.Login == user.Login select u).First().Days.ToList());
        }

        public ActionResult DeleteDay(Days day)
        {
            return View(day);
        }

        [HttpPost]
        [ActionName("DeleteDay")]
        public ActionResult DeleteD(Days day)
        {
            var day2 = db.Days.FirstOrDefault(d => d.ID_Day == day.ID_Day);
            try
            {
                db.Days.Remove(day2);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            string login = Session["Login"].ToString();
            return RedirectToAction("Calendar", (from u in db.Users where u.Login == login select u).First());
        }
    }
}
