using ArtGallery.App_Start;
using ArtGallery.DBaccess;
using ArtGallery.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ArtGallery.Controllers
{
    public class HomeController : Controller
    {

        private ArtifexContext db = new ArtifexContext();
        // GET: Manage
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //this view should return an event TITLE to EventViwer Action
        [Authorize]
        public ActionResult Events(int? page)
        {
            IPagedList<Event> events = db.GetEvents().ToPagedList(page ?? 1, 5);
            return View(events);

        }

        [Authorize]
        public ActionResult EventViwer(string EventTitle)
        {
            List<Event> e = db.GetEventInfo(EventTitle);
            ViewBag.image = e[0].IMAGE;
            ViewBag.info = e[0].INFO;
            ViewBag.loc = e[0].LOCATION;
            ViewBag.Title = e[0].TITLE;
            ViewBag.price = e[0].TICKET_PRICE;
            return View();
        }
        [Authorize]
        public ActionResult Buy(string title) {

            db.UpdateEvent(title);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
           public ActionResult ExploreArtworks(int? page)
        {
             ViewBag.Message = "Explore Artworks";
            
            IPagedList<Artwork> ArtWorks = db.GetArtworks().ToPagedList(page ?? 1, 5);
            return View(ArtWorks);
        }

        public ActionResult ArtworkView(string ArtworkTitle)
        {
            List<Artwork> e = db.GetArtworkInfo(ArtworkTitle);
            ViewBag.image = e[0].PHOTO;
            ViewBag.category = e[0].CATEGORY_NAME;
            ViewBag.ArtistUName = e[0].ARTIST_UNAME;
            ViewBag.name = e[0].TITLE;
            ViewBag.discription = e[0].DESCRIPTION;
            ViewBag.width = e[0].WIDTH;
            ViewBag.height = e[0].HEIGHT;
            ViewBag.depth = e[0].DEPTH;
            ViewBag.price = e[0].PRICE;
            ViewBag.medium = e[0].MEDIUM;
            ViewBag.subject = e[0].SUBJECT;
            ViewBag.material = e[0].MATERIAL;
            ViewBag.year = e[0].YEAR;
            if (!e[0].STATUS)
                ViewBag.status = "Sold";
            else
                ViewBag.status = "Available";
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