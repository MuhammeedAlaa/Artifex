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
            string imagefilename ="";
            if (ModelState.IsValid)
            {

                
                if (newuser.imagefile != null)
                {
                    imagefilename = Path.GetFileNameWithoutExtension(newuser.imagefile.FileName);
                    string extension = Path.GetExtension(newuser.imagefile.FileName);
                    imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                    newuser.PROFILE_PIC = "~/Image/" + imagefilename;
                    imagefilename = Path.Combine(Server.MapPath("~/Image/"), imagefilename);
                   
                   
                }
                if (db.SignUp(newuser) == 0)
                {
                    ModelState.AddModelError("", "Invalid signup attempt.");
                    return View();
                }
                else if(newuser.imagefile != null)
                    newuser.imagefile.SaveAs(imagefilename);

                ModelState.Clear();
                User u = new User
                {
                    USER_NAME = newuser.USER_NAME,
                    EMAIL = newuser.EMAIL,
                    PASSWORD = newuser.PASSWORD,
                    PHONE = newuser.PHONE,
                    FNAME = newuser.FNAME,
                    MINIT = newuser.MINIT,
                    LNAME = newuser.LNAME,
                    PROFILE_PIC = newuser.PROFILE_PIC
                };
                TempData["User"] = u;
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
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();

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