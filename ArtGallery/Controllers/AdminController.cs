using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGallery.ViewModels;

namespace ArtGallery.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            var admin = new AdminViewModel();
            admin.Admin.Name = "nice man";
            return View(admin);
        }
    }
}