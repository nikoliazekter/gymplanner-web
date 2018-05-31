using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class AdminController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Admin()
        {
            return View(db.Exercises.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Exercises exercise)
        {
            try
            {
                if (db.Exercises.FirstOrDefault(e => e.Name == exercise.Name) != null)
                {
                    ModelState.AddModelError("", "Вправа з такою назвою вже існує!");
                    return View(exercise);
                }
                if (ModelState.IsValid)
                    db.Exercises.Add(exercise);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            catch (Exception)
            {
                return View(exercise);
            }
        }

        public ActionResult Delete(string exerciseName) {
            return View(db.Exercises.FirstOrDefault(e => e.Name == exerciseName));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(string exerciseName)
        {
            var exercise2 = db.Exercises.FirstOrDefault(e => e.Name == exerciseName);
            try
            {
                db.Exercises.Remove(exercise2);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Admin", db.Exercises.ToList());
        }
    }
}