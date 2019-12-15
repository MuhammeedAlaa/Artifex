using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGallery.DBaccess;
using ArtGallery.ViewModels;
using db_access;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace ArtGallery.Controllers
{
    public class AdminController : Controller
    {
        
         AdminViewModel admin = new AdminViewModel();
         ArtifexContext db = new ArtifexContext();
        // GET: Admin

        public ActionResult Index()
        {
            
            admin.Admin.Name = "nice man";
            return View(admin);
        }

        public ActionResult Orders(string Orderby, int? page, bool asc = true)
        {

            if (Orderby == "OrderDate")
            {
                admin.Orders = db.GetSortedOrders("ORDER_DATE", asc).ToPagedList(page??1, 6);
                return View(admin);
            }
            else if (Orderby == "deliveryDate")
            {
                admin.Orders = db.GetSortedOrders("DELIVERY_DATE", asc).ToPagedList(page ?? 1, 1);
                return View(admin);
            }
            else
            {
                admin.Orders = db.GetSortedOrders("STATUS", asc).ToPagedList(page ?? 1, 1);
                return View(admin);
            }
        }
    }
}