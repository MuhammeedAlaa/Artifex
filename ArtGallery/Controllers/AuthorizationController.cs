using ArtGallery.Models;
using ArtGallery.ViewModels;
using System;
using System.IO;
using System.Web.Mvc;
using ArtGallery.DBaccess;
using System.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using ArtGallery.App_Start;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ArtGallery.Controllers
{
    public class AuthorizationController : Controller
    {
        private ArtifexContext db ;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AuthorizationController()
        {
            db = new ArtifexContext();
        }

        public AuthorizationController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // GET: Authorization
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public ActionResult SignUp(RegisterViewModel newuser)
        {
            string imagefilename = "";
            if (ModelState.IsValid)
            {

                if (newuser.imagefile != null)
                {
                    imagefilename = Path.GetFileNameWithoutExtension(newuser.imagefile.FileName);
                    string extension = Path.GetExtension(newuser.imagefile.FileName);
                    imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                    newuser.PROFILE_PIC = "~/Images/" + imagefilename;
                    imagefilename = Path.Combine(Server.MapPath("~/Images/"), imagefilename);
                }
                RegisterViewModel u = new RegisterViewModel
                {
                    USER_NAME = newuser.USER_NAME,
                    EMAIL = newuser.EMAIL,
                    PASSWORD = newuser.PASSWORD,
                    PHONE = newuser.PHONE,
                    FNAME = newuser.FNAME,
                    MINIT = newuser.MINIT,
                    LNAME = newuser.LNAME,
                    PROFILE_PIC = newuser.PROFILE_PIC,
                    imagefile = newuser.imagefile
                };
                TempData["User"] = u;
                TempData["imagepath"] = imagefilename;
                return RedirectToAction("BillingForm", "Authorization");
            }
            else
            {
                return View(newuser);
            }


        }

        //this is used to check whether the name is already taken when signing up with ajax reuquest
        public JsonResult IsUserNameAvailable(string USER_NAME)
        {
            return Json(db.UserNameAvailable(USER_NAME), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  ActionResult SignIn(LoginViewModel logeduser)
        {
            if (!ModelState.IsValid)
            {
                return View(logeduser);
            }
            DataTable t = db.SignIn(logeduser);
            if (t == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(logeduser);
            }
            else
            {
                string Email = Convert.ToString(t.Rows[0]["EMAIL"]);
                string Fname = Convert.ToString(t.Rows[0]["FNAME"]);
                string password = Convert.ToString(t.Rows[0]["PASSWORD"]);
                string Minit = Convert.ToString(t.Rows[0]["MINIT"]);
                string Lname = Convert.ToString(t.Rows[0]["LNAME"]);
                string Phone = Convert.ToString(t.Rows[0]["PHONE"]);
                string imagepath = Convert.ToString(t.Rows[0]["PROFILE_PIC"]);
                string username = Convert.ToString(t.Rows[0]["USER_NAME"]);
                User inputUser = new User
                {
                    EMAIL = Email,
                    USER_NAME = username,
                    FNAME = Fname,
                    MINIT = Minit,
                    PASSWORD = password,
                    LNAME = Lname,
                    PHONE = Phone,
                    PROFILE_PIC = imagepath
                };

                TempData["User"] = inputUser;
                System.Web.Security.FormsAuthentication.SetAuthCookie(logeduser.email, false);
                return RedirectToAction("Index", "Home", true);
            }
        }
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult BillingForm() 
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BillingForm(BillingInfo b) 
        {
            if (ModelState.IsValid)
            {
                RegisterViewModel newuser = (RegisterViewModel)TempData["User"];
                string path = (string)TempData["imagepath"];
                if (db.SignUp(newuser) == 0)
                {
                    ModelState.AddModelError("", "Invalid signup attempt.");
                    return View();
                }
                else if (newuser.imagefile != null)
                    newuser.imagefile.SaveAs(path);

                ModelState.Clear();
                System.Web.Security.FormsAuthentication.SetAuthCookie(newuser.EMAIL, false);
                return RedirectToAction("index", "manage");
            }
            else
                return View(b);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ApplyArtist() 
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ApplyExpert()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyArtist(Artist a)
        {

            string Email = User.Identity.Name;
            db.InsertArtist(Email, a.BIO, a.BYEAR,a.START_SALARY,a.END_SALARY);

            return RedirectToAction("index","manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyExpert(ExpertViewModel e)
        {
            string Email = User.Identity.Name;
            db.InsertExpert(Email, e.BIO, e.QUALIFICATIONS, e.BYEAR);
            return RedirectToAction("index", "manage");
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
                if(db != null)
                {

                    db.Dispose();
                    db = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}