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
using System.Data;
using System.IO;

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
            if (ModelState.IsValid)
            {
                db.UpdatePassword(User.Identity.Name, p);
            }
            return RedirectToAction("Index","manage");
        }

        //
        // GET: /Manage/Index
        [Route("Manage/Index/{Uname?}")]
        public ActionResult Index(string Uname)
        {
            string Email = User.Identity.Name;
            ViewBag.exp = null;
            ViewBag.artist = null;
            if (Email == "" && Uname == "")
                return RedirectToAction("SignIn", "Authorization");
            string un = db.GetUserName(Email);
            if (Email != "" && Uname == "" || (Email == Uname))
            {
                ViewBag.title = un;
                ExpertViewModel e = db.GetExpert(Email);

                ViewBag.imagepath = db.ProfileImagePath(Email);

                Artist a = db.GetArtist(Email);
                if (e != null)
                    ViewBag.exp = e;
                else
                    ViewBag.exp = null;
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

                ExpertViewModel e = db.GetExpert(Email);

                ViewBag.imagepath = db.ProfileImagePath(Email);

                Artist a = db.GetArtist(Email);
                if (e != null)
                    ViewBag.exp = e;
                else
                    ViewBag.exp = null;
                if (a != null)
                    ViewBag.artist = a;
                else
                    ViewBag.artist = null;
                if (ViewBag.imagepath == "")
                    ViewBag.imagepath = "/Images/def.png";
                return View();
            }

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadArt(Artwork a) 
        {
            string imagefilename = "";
            if (ModelState.IsValid)
            {

                if (a.imagefile != null)
                {
                    imagefilename = Path.GetFileNameWithoutExtension(a.imagefile.FileName);
                    string extension = Path.GetExtension(a.imagefile.FileName);
                    imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                    a.PHOTO = "~/Images/" + imagefilename;
                    imagefilename = Path.Combine(Server.MapPath("~/Images/"), imagefilename);
                    a.imagefile.SaveAs(imagefilename);
                }
                a.ARTIST_UNAME = db.GetUserName(User.Identity.Name);
                db.InsertArtwork(a);
                return RedirectToAction("Index", "Home");
            }
            return View(a);
        }
        [Authorize]
        public ActionResult UploadArt(string Username)
        {

            ViewBag.Uname = Username;
            DataTable b = db.GetCategories();
            if(b != null)
            {
                ViewBag.cat = b.AsEnumerable().Select(row => new Category
                {
                    NAME = row["NAME"].ToString()
                }
                );
            }
            return View();
        }




        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomOrder(CustomOrderUserViewModel c)
        {
            if (ModelState.IsValid)
            {
                db.InsertCustomOrder(c);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [Authorize]
        public ActionResult CustomOrder() 
        {

            DataTable cat = db.GetCategories();
            if (cat != null)
            {
                ViewBag.cat = cat.AsEnumerable().Select(row => new Category
                {
                    NAME = row["NAME"].ToString()
                }
                );
            }
            return View();
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