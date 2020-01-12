

using LuvDating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuvDating.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var viewModel = new HomeIndexViewModel();
            if (db.Users.Count() > 4)
            {
                viewModel = new HomeIndexViewModel { UserList = db.Users.Take(4).ToList() };
            }
            else { viewModel = new HomeIndexViewModel { UserList = db.Users.ToList() }; }
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}