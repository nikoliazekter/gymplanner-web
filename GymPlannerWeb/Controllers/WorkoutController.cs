using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class WorkoutController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Workout(Workouts workout)
        {
            Session["ID_Workout"] = workout.ID_Workout;
            return View((from w in db.Workouts where w.ID_Workout == workout.ID_Workout select w).First());
        }

        public ActionResult _AddSet(Sets set)
        {
            return PartialView(set);
        }

        [HttpPost]
        [ActionName("_AddSet")]
        public ActionResult _AddSetPost(Sets set)
        {
            int workoutId = (int)Session["ID_Workout"];
            var workout = db.Workouts.FirstOrDefault(w => w.ID_Workout == workoutId);
            try
            {
                if (ModelState.IsValid)
                {
                    set.Workouts.Add(workout);
                    db.Sets.Add(set);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
            return PartialView(set);
        }

        public ActionResult Delete(Sets set)
        {
            int workoutId = (int)Session["ID_Workout"];
            var workout = db.Workouts.FirstOrDefault(w => w.ID_Workout == workoutId);
            var set2 = db.Sets.FirstOrDefault(s => s.ID_Set == set.ID_Set);
            try
            {
                db.Sets.Remove(set2);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Workout", workout);
        }
    }
}