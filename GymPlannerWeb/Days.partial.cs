﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb
{
    [MetadataType(typeof(DaysMD))]
    partial class Days
    {
        public class DaysMD
        {
            [HiddenInput(DisplayValue = false)]
            public int ID_Day { get; set; }

            [Display(Name = "Дата")]
            [DataType(DataType.Date)]
            public System.DateTime Date { get; set; }

            [Display(Name = "Коментар")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Comment { get; set; }
        }
    }
}