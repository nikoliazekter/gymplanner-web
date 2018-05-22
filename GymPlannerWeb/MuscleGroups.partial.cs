using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace GymPlannerWeb
{
    [MetadataType(typeof(MuscleGroupsMD))]
    partial class MuscleGroups
    {
        public class MuscleGroupsMD
        {
            [Display(Name = "Назва")]
            public string Name { get; set; }

            [Display(Name = "Інформація")]
            public string Information { get; set; }
        }
    }
}