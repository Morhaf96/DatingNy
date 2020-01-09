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

            try
            {
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
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEditViewModel model)
        {
            var db = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var userInfo = db.Users.FirstOrDefault(p => p.Id == userId);

            userInfo.Email = model.Email;
            userInfo.UserName = model.Email;
            userInfo.Name = model.Name;
            userInfo.BirthDate = model.Birth;
            userInfo.Gender = model.Gender;
            
            if (ModelState.IsValid)
            {
                db.SaveChanges();

                return RedirectToAction("Index", "Profile");
            }
            return View(model);
        }


        public ActionResult UserProfile(string id)
        {
            var db = new ApplicationDbContext();
            var chosenProfile = db.Users.FirstOrDefault(p => p.Id == id);
            var currentUser = User.Identity.GetUserId();
            if (id == currentUser)
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