using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class SearchController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Search()
        {
            string login = Session["Login"].ToString();

            var workouts = (from w in db.Workouts select w);

            workouts = workouts.Where(w => w.Days.Any(d => d.Users.Any(u => u.Login == login)));

            var workoutPlusDay = (from w in workouts
                                  select
               new
               {
                   day = w.Days.FirstOrDefault().Date,
                   workout = w.Exercises.FirstOrDefault().Name
               });

            ViewBag.workoutsList = new List<(string, string)>();

            foreach (var wpd in workoutPlusDay)
                ViewBag.workoutsList.Add((wpd.day.ToShortDateString(), wpd.workout));

            return View();
        }

        [HttpPost]
        public ActionResult Search(string dateStart, string dateEnd, int? reps, int? minWeight, int? maxWeight)
        {
            //this.workoutsListView.Items.Clear();

            string login = Session["Login"].ToString();
            DateTime startDate;
            DateTime endDate;
            if (string.IsNullOrEmpty(dateStart))
                startDate = DateTime.Today.AddYears(-1);
            else
                startDate = DateTime.Parse(dateStart);

            if (string.IsNullOrEmpty(dateEnd))
                endDate = DateTime.Today;
            else
                endDate = DateTime.Parse(dateEnd);

            var workouts = (from w in db.Workouts select w);

            workouts = workouts.Where(w => w.Days.Any(d => d.Users.Any(u => u.Login == login)));

            workouts = workouts.Where(w => w.Days.Any(d => d.Date.CompareTo(endDate) <= 0  && d.Date.CompareTo(startDate) >= 0 && d.Workouts.Count() > 0));


            if (Session["Selected"] != null && !string.IsNullOrEmpty(Session["Selected"].ToString()))
            {
                var exercise = Session["Selected"].ToString();
                workouts = workouts.Where(w => w.Exercises.Any(ex => ex.Name == exercise));
            }
            if (reps != null)
                workouts = workouts.Where(w => w.Sets.Any(s => s.Num_Reps == reps));

            if (maxWeight != null)
                workouts = workouts.Where(w => w.Sets.Any(s => s.Weight <= maxWeight));

            if (minWeight != null)
                workouts = workouts.Where(w => w.Sets.Any(s => s.Weight >= minWeight));

            var workoutPlusDay = (from w in workouts
                                  select
               new
               {
                   day = w.Days.FirstOrDefault().Date,
                   workout = w.Exercises.FirstOrDefault().Name
               });

            ViewBag.workoutsList = new List<(string, string)>();

            foreach (var wpd in workoutPlusDay)
                ViewBag.workoutsList.Add((wpd.day.ToShortDateString(), wpd.workout));
            
            return View();
        }
    }
}