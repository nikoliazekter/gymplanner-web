using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymPlannerWeb.Controllers
{
    public class UsersController : Controller
    {
        private NewGymPlannerEntities db = new NewGymPlannerEntities();
        // GET: Users
        public ActionResult UsersRender()
        {
            return View(db.Users.ToList());
        }
    }
}