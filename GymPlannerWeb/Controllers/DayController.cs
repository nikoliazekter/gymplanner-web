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

        public ActionResult Day(int dayId)
        {
            if (dayId == -1)
                dayId = (int)Session["ID_Day"];
            else
                Session["ID_Day"] = dayId;
            return View((from d in db.Days where d.ID_Day == dayId select d).First().Workouts.ToList());
        }

        public ActionResult CreateWorkout()
        {
            return View();
        }

        public ActionResult DeleteWorkout(Workouts workout)
        {
            return View((from w in db.Workouts where w.ID_Workout == workout.ID_Workout select w).First());
        }

        [HttpPost]
        [ActionName("DeleteWorkout")]
        public ActionResult DeleteW(Workouts workout)
        {
            var workout2 = db.Workouts.FirstOrDefault(w => w.ID_Workout == workout.ID_Workout);
            try
            {
                db.Workouts.Remove(workout2);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Day", (int)Session["ID_Day"]);
        }
    }
}