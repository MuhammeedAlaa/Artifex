using ArtGallery.Models;
using ArtGallery.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGallery.DBaccess;

namespace ArtGallery.Controllers
{
    public class AuthorizationController : Controller
    {
        private ArtifexContext db = new ArtifexContext();
        // GET: Authorization
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp() {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User newuser) {
            newuser.PROFILE_PIC = "~/Image/" + newuser.PROFILE_PIC;
            db.SignUp(newuser);
            if (newuser.imagefile != null)
            {
                string imagefilename = Path.GetFileNameWithoutExtension(newuser.imagefile.FileName);
                string extension = Path.GetExtension(newuser.imagefile.FileName);
                imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                newuser.PROFILE_PIC = "~/Image/" + imagefilename;
                imagefilename = Path.Combine(Server.MapPath("~/Image/"), imagefilename);
                newuser.imagefile.SaveAs(imagefilename);
                ModelState.Clear();
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult  SignIn() {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(LoginViewModel logeduser) {

            return View();
        }



    }
}