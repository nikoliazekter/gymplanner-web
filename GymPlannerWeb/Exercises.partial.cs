using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb
{
    [MetadataType(typeof(ExercisesMD))]
    partial class Exercises
    {
        public class ExercisesMD
        {
            [Display(Name = "Назва")]
            public string Name { get; set; }

            [Display(Name = "Інформація")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Information { get; set; }
        }
    }
}