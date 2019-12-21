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
using PagedList;

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
        public ActionResult Index(int? page, int? page2, string Uname)
        {
            string Email = User.Identity.Name;
            ViewBag.exp = null;
            ViewBag.artist = null;
            IPagedList<Artwork> AW;
            ViewBag.artist = null;
            List<Artwork> temp = new List<Artwork>();
            AW = temp.ToPagedList(page ?? 1, 5);
            ViewBag.same = false;
            if (Email == "" && Uname == null)
                return RedirectToAction("SignIn", "Authorization");
            string un = db.GetUserName(Email);
            if (Email != "" && Uname == null || (Email == Uname))
            {
                ViewBag.same = true;
                ViewBag.title = un;
                List<ExpertViewModel> e = db.GetExpert(Email);

                ViewBag.imagepath = db.ProfileImagePath(Email);

                Artist a = db.GetArtist(Email);
                if (e != null)
                {
                    ViewBag.reqlist = db.GetRequestedSurvey(e[0]).ToPagedList(page2 ?? 1, 5);
                    ViewBag.exp = e;
                }
                else
                    ViewBag.exp = null;
                if (a != null)
                {
                    ViewBag.artist = a;
                    AW = db.GetArtWorks(a).ToPagedList(page ?? 1, 5);
                }
                else
                    ViewBag.artist = null;
                if (ViewBag.imagepath == "")
                    ViewBag.imagepath = "/Images/def.png";
                if (a != null)
                    return View(AW);
                else
                    return View();
            }
            else //if(Email == null && Uname != "" || (Email != null && Uname != "") )
            {
                Email = Uname;
                ViewBag.title = db.GetUserName(Email);

                List<ExpertViewModel> e = db.GetExpert(Email);

                ViewBag.imagepath = db.ProfileImagePath(Email);

                Artist a = db.GetArtist(Email);
                if (e != null)
                {
                    ViewBag.reqlist = db.GetRequestedSurvey(e[0]).ToPagedList(page2 ?? 1, 5);
                    ViewBag.exp = e;
                }
                else
                    ViewBag.exp = null;
                if (a != null)
                {
                    AW = db.GetArtWorks(a).ToPagedList(page ?? 1, 5);
                    ViewBag.artist = a;
                }
                else
                    ViewBag.artist = null;
                if (ViewBag.imagepath == "")
                    ViewBag.imagepath = "/Images/def.png";
                return View(AW);
            }

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UArt(Artwork a) 
        {
             string imagefilename = "";
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
        [Authorize]
        [Route("Manage/UploadArt/{Username?}")]
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
        [Route("Manage/AddQual/{Username ?}")]
        public ActionResult AddQual(string Username)
        {
            ViewBag.Uname = Username;
            return View();
        }

        [Authorize]
        [HttpPost]
        
        public ActionResult InsertQual( ExpertViewModel e)
        {
            e.EXPERT_UNAME = (string)TempData["expert"];
            db.InsertQual(e);
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        [Route("Manage/AddSurvey/{Username?}")]
        public ActionResult AddSurvey(string Username)
        {
            ViewBag.Uname = Username;
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Manage/InsertSurvey/{Username?}")]
        public ActionResult InsertSurvey(Survey s,string Username)
        {
            s.EXPERT_UNAME = Username;
            s.USER_NAME = db.GetUserName(User.Identity.Name);
            db.InsertSurveyRequest(s);
            return RedirectToAction("Index","Home");
        }


        [Authorize]
        [Route("Manage/ArtViwer/{Code?}")]
        public ActionResult ArtViwer(string Code)
        {
            Artwork a = db.GetArtWorkInfo(Convert.ToInt32(Code))[0];
            return View(a);
        }
        [Authorize]
        [Route("Manage/SurveyRequestAction/{Code?}")]
        public ActionResult SurveyRequestAction(string Code)
        {
            Survey s  = db.GetSurveyInfo(Convert.ToInt32(Code));
            return View(s);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SurveyResponse(string Code)
        {
            int surveyid = (int)Session["ID"];
            string[] x = (string[])Session["selected"];
            Survey s = db.GetSurveyInfo(Convert.ToInt32(Code));
            return View(s);
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
        [Authorize]
        public ActionResult Favourite(int? page)
        {
            ViewBag.Message = "Favourite Artworks";

            IPagedList<Artwork> ArtWorks = db.GetFavourite(User.Identity.Name).ToPagedList(page ?? 1, 5);
            return View(ArtWorks);
        }

        public ActionResult LoadData()
        {
            var data = db.GetArtworksforrecommanded();
            foreach(var l in data)
            {
                l.PHOTO = l.PHOTO.Substring(1);
            }
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveSelected(string[] list)
        {
            Session["selected"] = list;
            return Json(TempData["selected"]);
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