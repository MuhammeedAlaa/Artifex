using ArtGallery.App_Start;
using ArtGallery.DBaccess;
using ArtGallery.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGallery.ViewModels;
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
            int result;
            int.TryParse(User.Identity.Name.ToString(), out result);
            if (result != 0)
             return   RedirectToAction("index", "admin");
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult rate(int rating) {
            Artwork a = (Artwork)TempData["buyartwork"];
            db.rateArtwork(rating,a.AW_CODE,db.GetUserName(User.Identity.Name));

            return RedirectToAction("Index", "Home");
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
            List<Event> ev = db.GetEvents();
            List<Event> realevents = new List<Event>();
            foreach (var even in ev)
            {
                if(even.TICKETS_NUM > 0)
                    realevents.Add(even);
            }
            IPagedList<Event> events = realevents.ToPagedList(page ?? 1, 5);
            return View(events);

        }

        [Authorize]
        public ActionResult EventViwer(string EventTitle)
        {
            List<Event> e = db.GetEventInfo(EventTitle);

            return View(e[0]);
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
            ViewBag.same = db.GetEmail(e[0].ARTIST_UNAME) == User.Identity.Name;
            if (!e[0].STATUS)
                ViewBag.status = "Sold";
            else
                ViewBag.status = "Available";
            e[0].STS = ViewBag.status;
            return View(e[0]);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Home/fav/{rating?}")]
        public ActionResult fav(int rating) 
        {
            Artwork a = (Artwork)TempData["buyartwork"];
            db.addFav(a.AW_CODE, db.GetUserName(User.Identity.Name),rating);

            return RedirectToAction("index", "home");
        }

        public ActionResult Experts(int? page)
        {
            ListExpViewModel ex = new ListExpViewModel();
            ex.Experts = db.GetExperts().ToPagedList(page ?? 1, 10);
            ex.Emails = db.GetExpertMails();
            return View(ex);
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