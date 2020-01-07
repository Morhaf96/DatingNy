using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuDating.Models;
using Microsoft.AspNet.Identity;

namespace LuDating.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var userInfo = db.Users.FirstOrDefault(a => a.Id == userId);

            return View(new ProfileIndexViewModel
            {
                AccountId = userId,
                Name = userInfo.Name,
                Birth = userInfo.BirthDate,
                Gender = userInfo.Gender,
                Bio = userInfo.Bio,
                Image = userInfo.ImageUrl,
            });
        }
    }
}