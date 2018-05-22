using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb
{
    [MetadataType(typeof(SetsMD))]
    partial class Sets
    {
        public class SetsMD
        {
            [HiddenInput(DisplayValue = false)]
            public int ID_Set { get; set; }

            [Display(Name = "К-сть повторів")]
            public int Num_Reps { get; set; }

            [Display(Name = "Вага")]
            public decimal Weight { get; set; }
        }
    }
}