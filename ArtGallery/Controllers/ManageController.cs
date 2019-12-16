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

        //
        // GET: /Manage/Index
        [Authorize]
        public ActionResult Index()
        {
           
            string Email = User.Identity.Name;
            
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
<<<<<<< HEAD

=======
>>>>>>> eb18f290d0d40984de3dda33ae1a02a56f8c68f7
            return View();
        }

    }
}