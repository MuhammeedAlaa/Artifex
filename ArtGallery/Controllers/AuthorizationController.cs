using ArtGallery.Models;
using ArtGallery.ViewModels;
using System;
using System.IO;
using System.Web.Mvc;
using ArtGallery.DBaccess;
using System.Data;

namespace ArtGallery.Controllers
{
    public class AuthorizationController : Controller
    {
        private ArtifexContext db = new ArtifexContext();
        // GET: Authorization
        public ActionResult Index()
        {
            User inputUser = (User)TempData["User"];
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignUp() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegisterViewModel newuser) {
            if (ModelState.IsValid)
            {

                if (newuser.imagefile != null)
                {
                    string imagefilename = Path.GetFileNameWithoutExtension(newuser.imagefile.FileName);
                    string extension = Path.GetExtension(newuser.imagefile.FileName);
                    imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                    newuser.PROFILE_PIC = "~/Image/" + imagefilename;
                    imagefilename = Path.Combine(Server.MapPath("~/Image/"), imagefilename);
                    string hash = (newuser.PASSWORD);
                    if (db.SignUp(newuser) == 0)
                    {
                        return RedirectToAction("SignUp", "Authorization");
                    }
                    else
                        newuser.imagefile.SaveAs(imagefilename);
                }

                ModelState.Clear();
                TempData["User"] = newuser;
                return RedirectToAction("Index", "Authorization");
            }
            else
            {
                return View(newuser);
            }

            
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult  SignIn() {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginViewModel logeduser) {
            if (ModelState.IsValid)
            {
          
                DataTable t = db.SignIn(logeduser);
                if (t == null)
                    return RedirectToAction("SignIn","Authorization");
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
                    return RedirectToAction("Index", "Authorization");
                }
            }
            else
            {
                return View(logeduser);
            }

        
        }



    }
}