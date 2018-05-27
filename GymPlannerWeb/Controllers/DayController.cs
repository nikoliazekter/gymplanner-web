using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class DayController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Day(Days day)
        {
            return View((from d in db.Days where d.ID_Day == day.ID_Day select d).First().Workouts.ToList());
        }
    }
}