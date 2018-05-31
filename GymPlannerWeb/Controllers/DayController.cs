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
            return View(db.Exercises.ToList());
        }

        [HttpPost]
        public ActionResult CreateWorkout(string exerciseName)
        {
            foreach (var exercise in db.Exercises.ToList())
            {
                if (exercise.Name.ToString().ToLower() == exerciseName.ToLower())
                {
                    Workouts workout = new Workouts
                    {
                        Num_Sets = 0
                    };
                    workout.Exercises.Add(db.Exercises.FirstOrDefault(ex => ex.Name == exerciseName));
                    int dayId = (int)Session["ID_Day"];
                    workout.Days.Add(db.Days.FirstOrDefault(d => d.ID_Day == dayId));
                    db.Workouts.Add(workout);
                    db.SaveChanges();
                    return RedirectToAction("Workout", "Workout", workout);
                }
            }
            return View(db.Exercises.ToList());
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