using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace GymPlannerWeb.Controllers
{
    public class StatController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();
        public ActionResult Index()
        {
            return View("Stat");
        }

        public ActionResult Stat()
        {
            List<string> exercises = new List<string>();

            if (Session["Selected"] != null && Session["Selected"].ToString() != "")
                exercises.Add(Session["Selected"].ToString());
            else
                exercises.Add("Жим ногами");

            var chart = new Chart();
            chart.Width = 800;
            chart.Height = 500;
            string userName = Session["Login"].ToString();
            foreach (string exerciseName in exercises)
            {
                var records = (from d in db.Days.Where(d => d.Users.Any(u => u.Login == userName) && d.Workouts.Any(w => w.Exercises.Any(ex => ex.Name == exerciseName)))
                               group d by d.ID_Day into g
                               select new
                               {
                                   date = (from d in g select d.Date).FirstOrDefault(),
                                   record = (from w in (from day in g select day.Workouts.FirstOrDefault())
                                             select w.Sets.Max(s => s.Weight)).Max()
                               }).ToList();

                chart.Series.Add(exerciseName);
                chart.Series[exerciseName].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                foreach (var record in records)
                    chart.Series[exerciseName].Points.AddXY(record.date, record.record);
                chart.Legends.Add(new Legend(exerciseName) { Docking = Docking.Right });
            }
            chart.ChartAreas.Add(new ChartArea());
            chart.ChartAreas[0].AxisX.Title = "Час";
            chart.ChartAreas[0].AxisX.ArrowStyle = System.Web.UI.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd-MM-yy";
            chart.ChartAreas[0].AxisY.Title = "Вага";
            chart.ChartAreas[0].AxisY.ArrowStyle = System.Web.UI.DataVisualization.Charting.AxisArrowStyle.Triangle;

            using (var ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms.ToArray(), "image/png");
            }
        }
    }
}