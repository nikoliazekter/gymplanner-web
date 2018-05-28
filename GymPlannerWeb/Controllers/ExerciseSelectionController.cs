using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class ExerciseSelectionController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();
        // GET: ExerciseSelection
        public ActionResult Index()
        {
            return View("SelectExercise", db.Exercises.ToList());
        }
        public ActionResult Select()
        {
            return null;
        }
        public  ActionResult Text_changed()
        {
            var exercises = new List<string>();
            foreach (var exercise in db.Exercises.ToList())
            {
                if (exercise.ToString().ToLower().Contains(Request.Form["input1"].ToLower()))
                    exercises.Add(exercise.ToString());
            }
            return View("SelectExercise", exercises);
        }
    }
}