using LuvDating.Models;
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

                        string filename = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                        file.SaveAs(path);
                        //lägger in bilden till den personen.
                        var db = new ApplicationDbContext();
                        var userId = User.Identity.GetUserId();
                        var userInfo = db.Users.FirstOrDefault(a => a.Id == userId);

                        //kollar om användaren hade en bild innan ta bort den isåfall.
                        if (userInfo.ImageName != null)
                        {
                            string oldImagePath = Path.Combine(Server.MapPath("~/UploadedFiles"), userInfo.ImageName);
                            DeleteFileFromFolder(oldImagePath);
                        }

                        //sparar 
                        userInfo.ImageName = Path.GetFileName(file.FileName);
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

        public void DeleteFileFromFolder(string path)
        {

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

        }
    }
}