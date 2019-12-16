using ArtGallery.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ArtGallery.DBaccess;
using ArtGallery.Models;
using ArtGallery.ViewModels;

namespace ArtGallery.Controllers
{
    public class ManageController : Controller
    {
        private ArtifexContext db = new ArtifexContext();
        // GET: Manage
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword() {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel p)
        {
            db.UpdatePassword(User.Identity.Name, p);

            return RedirectToAction("Index","manage");
        }

        //
        // GET: /Manage/Index
        [Route("Manage/{Uname?}")]
        public ActionResult Index(string Uname)
        {
           
            string Email = User.Identity.Name;
            ViewBag.expert = null;
            ViewBag.artist = null;

            if (Email == "" && Uname == null)
                return RedirectToAction("SignIn", "Authorization");
            string un = db.GetUserName(Email);
            if (Email != "" && Uname == "" || (Email == Uname))
            {

                ViewBag.title = un;
                List<ExpertViewModel> e = db.GetExpert(Email);

                ViewBag.imagepath = db.ProfileImagePath(Email);

                List<Artist> a = db.GetArtist(Email);
                if (e != null)
                    ViewBag.expert = e;
                else
                    ViewBag.expert = null;
                if (a != null)
                    ViewBag.artist = a;
                else
                    ViewBag.artist = null;


                if (ViewBag.imagepath == "")
                    ViewBag.imagepath = "/Images/def.png";
                return View();
            }
            else //if(Email == null && Uname != "" || (Email != null && Uname != "") )
            {
                Email = Uname;
                ViewBag.title = Uname;

                List<ExpertViewModel> e = db.GetExpert(Email);

                ViewBag.imagepath = db.ProfileImagePath(Email);

                List<Artist> a = db.GetArtist(Email);
                if (e != null)
                    ViewBag.expert = e;
                else
                    ViewBag.expert = null;
                if (a != null)
                    ViewBag.artist = a;
                else
                    ViewBag.artist = null;


                if (ViewBag.imagepath == "")
                    ViewBag.imagepath = "/Images/def.png";
                return View();
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (db != null)
                {

                    db.Dispose();
                    db = null;
                }
            }

            base.Dispose(disposing);
        }

    }
}