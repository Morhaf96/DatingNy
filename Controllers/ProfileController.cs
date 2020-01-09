using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuvDating.Models;
using Microsoft.AspNet.Identity;

namespace LuvDating.Controllers
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
                Image = userInfo.ImageName,
            });
        }
        public ActionResult UserProfile(string profileId)
        {
            var db = new ApplicationDbContext();
            var chosenProfile = db.Users.FirstOrDefault(p => p.Id == profileId);
            var currentUser = User.Identity.GetUserId();
            if(profileId == currentUser)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(new ProfileIndexViewModel 
                {
                    AccountId = chosenProfile.Id,
                    Name = chosenProfile.Name,
                    Gender = chosenProfile.Gender,
                    Birth = chosenProfile.BirthDate,
                    Bio = chosenProfile.Bio,
                    Image = chosenProfile.ImageName

                });
            }
        }
    }
}