using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class ChooseController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Choose()
        {
            return View("Choose", db.Exercises.ToList());
        }

        [HttpPost]
        public ActionResult Choose(string exerciseName)
        {
            foreach (var exercise in db.Exercises.ToList())
            {
                if (exercise.Name.ToString().ToLower() == exerciseName.ToLower())
                {
                    Session["Selected"] = exercise.Name.ToString();
                    return RedirectToAction("index", "Stat");
                }
            }
            return View("Choose", db.Exercises.ToList());
        }
    }
}