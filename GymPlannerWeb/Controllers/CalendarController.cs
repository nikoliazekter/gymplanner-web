﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class CalendarController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();

        public ActionResult Calendar(Users user)
        {
            return View((from u in db.Users where u.Login == user.Login select u).First().Days.ToList());
        }
    }
}
