using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GymPlannerWeb
{
    [MetadataType(typeof(WorkoutsMD))]
    partial class Workouts
    {
        public class WorkoutsMD
        {
            [HiddenInput(DisplayValue = false)]
            public int ID_Workout { get; set; }

            [Display(Name = "К-сть підходів")]
            public int Num_Sets { get; set; }
        }
    }
}