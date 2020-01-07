using LuDating.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadingFilesUsingMVC.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload    
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        //lägger in bilden till den personen.
                        var db = new ApplicationDbContext();
                        var userId = User.Identity.GetUserId();
                        var userInfo = db.Users.FirstOrDefault(a => a.Id == userId);

                        userInfo.ImageUrl = path;
                        db.SaveChanges();


                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

            }
            return View("Index");
        }
    }
}