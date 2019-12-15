using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using ArtGallery.DBaccess;
using ArtGallery.ViewModels;
using db_access;
using Microsoft.Ajax.Utilities;
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

        public ActionResult Orders(string Orderby, int? page, string sortdir)
        {
            if (Request.QueryString["table_search"].IsNullOrWhiteSpace() || !Request.QueryString["table_search"].IsInt())
            {
                sortdir = sortdir == "desc" ? "asc" : "desc";
                bool asc = sortdir == "desc" ? true : false;
                if (Orderby == "OrderDate")
                {
                    admin.Orders = db.GetSortedOrders("ORDER_DATE", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else if (Orderby == "deliveryDate")
                {
                    admin.Orders = db.GetSortedOrders("DELIVERY_DATE", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else
                {
                    admin.Orders = db.GetSortedOrders("STATUS", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
            }
            else
            {
                admin.Orders = db.GetOrderById(Convert.ToInt32(Request.QueryString["table_search"])).ToPagedList(page ?? 1, 1);
                return View(admin);
            }
        }
    }
}