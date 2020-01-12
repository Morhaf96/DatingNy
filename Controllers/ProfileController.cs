using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuDating.Models;
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
        
        public ActionResult FriendRequest(string id)
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var recieverProfile = db.Users.FirstOrDefault(p => p.Id == id);
            var senderProfile = db.Users.FirstOrDefault(p => p.Id == currentUser);

                senderProfile.FriendList.Add(new FriendModel 
                { 
                    FriendRequestReciever = id,
                    Name = recieverProfile.Name,
                    pendingRequest = 0
                }); 
            

            var reciever = new FriendModel();
            
            reciever.Sender.Add(new ApplicationUser { Id = senderProfile.Id });

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DisplayFriendRequests()
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var senderProfile = db.Users.FirstOrDefault(p => p.Id == currentUser);
            var query = db.FriendModels.Where(p => p.FriendRequestReciever == currentUser && p.pendingRequest == 0).SelectMany(p => p.Sender).ToList();
            var list = new SenderListModel
            {
                RequestsFrom = query
            };

            return View(list);
        }

        public ActionResult AcceptRequest(string id)
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = db.FriendModels.FirstOrDefault(p => p.FriendRequestReciever == currentUser);
            var senderProfile = db.Users.FirstOrDefault(p => p.Id == id);
            var usr = currentProfile.Sender.ToList();
            var fren = senderProfile.FriendList.ToList();
            
           for( int i = 0;  i < usr.Count(); i++)
            {
                if (usr[i].Id == id && fren[i].FriendRequestReciever == currentUser && fren[i].pendingRequest == 0 )
                {
                    fren[i].pendingRequest = 1;
                    break;
                }
            }db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeclineRequest(string id)
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = db.FriendModels.FirstOrDefault(p => p.FriendRequestReciever == currentUser);
            var senderProfile = db.Users.FirstOrDefault(p => p.Id == id);
            var usr = currentProfile.Sender.ToList();
            var fren = senderProfile.FriendList.ToList();

            for (int i = 0; i < usr.Count(); i++)
            {
                if (usr[i].Id == id && fren[i].FriendRequestReciever == currentUser && fren[i].pendingRequest == 0)
                {
                    fren[i].pendingRequest = 2;
                    fren.Remove(fren[i]);
                    break;
                    
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DisplayFriends()
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var senderProfile = db.Users.FirstOrDefault(p => p.Id == currentUser);
            var currentProfile = db.FriendModels.FirstOrDefault(p => p.FriendRequestReciever == currentUser);
            var query = db.FriendModels.Where(p => p.FriendRequestReciever == currentUser && p.pendingRequest == 1).SelectMany(p => p.Sender).ToList();
            
            var lista = senderProfile.FriendList.ToList();

            var usr = new List<ApplicationUser>();
           
           
            if(currentProfile != null)
            {
                 usr = currentProfile.Sender.ToList();
                for (int i = 0; i < lista.Count(); i++)
                {
                    if (lista[i].pendingRequest == 1 && lista[i].FriendRequestReciever != currentUser && usr[i].Id == currentUser)
                    {
                        var profile = db.Users.FirstOrDefault(p => p.Id == lista[i].FriendRequestReciever);
                        query.Add(profile);
                    }
                }
            }
            else
            {
                for (int i = 0; i < lista.Count(); i++)
                {   
                    if (lista[i].pendingRequest == 1 && lista[i].FriendRequestReciever != currentUser)
                    {
                        var _id = lista[i].FriendRequestReciever;
                        var profile = db.Users.FirstOrDefault(p => p.Id == _id );
                        query.Add(profile);
                    }
                }
            }
           

            var list = new SenderListModel
            {
                RequestsFrom = query
            };

            return View(list);
        }

        public ActionResult DeleteFriend(string id)
        {
            var db = new ApplicationDbContext();
            var currentUser = User.Identity.GetUserId();
            var currentProfile = db.FriendModels.FirstOrDefault(p => p.FriendRequestReciever == currentUser);
            var senderProfile = db.Users.FirstOrDefault(p => p.Id == id);
            var usr = currentProfile.Sender.ToList();
            var fren = senderProfile.FriendList.ToList();

            for (int i = 0; i < usr.Count(); i++)
            {
                if (usr[i].Id == id && fren[i].FriendRequestReciever == currentUser && fren[i].pendingRequest == 1)
                {
                    fren[i].pendingRequest = 2;
                    fren.Remove(fren[i]);
                    break;

                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }


}