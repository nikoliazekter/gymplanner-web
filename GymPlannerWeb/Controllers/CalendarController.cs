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

        public ActionResult Calendar()
        {
            string login = Session["Login"].ToString();
            return View((from u in db.Users where u.Login == login select u).First().Days.Where(d => d.Workouts.Count > 0).ToList());
        }

        public ActionResult GoToDay(string date)
        {
            string login = Session["Login"].ToString();
            var dateTime = DateTime.Parse(date);
            var day = db.Days.FirstOrDefault(d => d.Users.Any(u => u.Login == login) && d.Date == dateTime);
            int id = -1;
            if (day == null)
            {
                Days newDay = new Days
                {
                    Date = dateTime,
                    Comment = ""
                };
                newDay.Users.Add(db.Users.FirstOrDefault(u => u.Login == login));
                db.Days.Add(newDay);
                db.SaveChanges();
                day = newDay;

            }
            id = day.ID_Day;
            return RedirectToAction("Day", "Day", new { dayId = id });
        }

        public ActionResult DeleteDay(int dayId)
        {
            return View((from d in db.Days where d.ID_Day == dayId select d).First());
        }

        [HttpPost]
        [ActionName("DeleteDay")]
        public ActionResult DeleteD(int dayId)
        {
            var day = db.Days.FirstOrDefault(d => d.ID_Day == dayId);
            try
            {
                db.Days.Remove(day);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            string login = Session["Login"].ToString();
            return RedirectToAction("Calendar");
        }
    }
}
