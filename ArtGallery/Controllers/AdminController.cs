using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using ArtGallery.Authorize;
using ArtGallery.DBaccess;
using ArtGallery.Models;
using ArtGallery.Statistics;
using ArtGallery.ViewModels;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace ArtGallery.Controllers
{
    [CustomAuthorizeAttribte]
    public class AdminController : Controller
    {

        AdminViewModel admin = new AdminViewModel();

        ArtifexContext db = new ArtifexContext();
        // GET: Admin
        public ActionResult Index()
        {
            List<string> l1 = new List<string>();
            List<string> d1 = new List<string>();
            DataTable t1 = db.NumAttendEvent();
            if (t1 != null)
            {
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    l1.Add(Convert.ToString(t1.Rows[i][0]));
                }
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    d1.Add(Convert.ToString(t1.Rows[i][1]));
                }

                ViewBag.l1 = l1;
                ViewBag.d1 = d1;
            }
            List<string> l2 = new List<string>();
            List<string> d2 = new List<string>();
            DataTable t2 = db.NumOrderShipped();
            if (t2 != null)
            {
                for (int i = 0; i < t2.Rows.Count; i++)
                {
                    l2.Add(Convert.ToString(t2.Rows[i][0]));
                }
                for (int i = 0; i < t2.Rows.Count; i++)
                {
                    d2.Add(Convert.ToString(t2.Rows[i][1]));
                }

                ViewBag.l2 = l2;
                ViewBag.d2 = d2;
            }
            List<string> l3 = new List<string>();
            List<string> d3 = new List<string>();
            DataTable t3 = db.NumArtwrkCategory();
            if (t3 != null)
            {
                for (int i = 0; i < t3.Rows.Count; i++)
                {
                    l3.Add(Convert.ToString(t3.Rows[i][0]));
                }
                for (int i = 0; i < t3.Rows.Count; i++)
                {
                    d3.Add(Convert.ToString(t3.Rows[i][1]));
                }

                ViewBag.l3 = l3;
                ViewBag.d3 = d3;
            }
            List<string> l4 = new List<string>();
            List<string> d4 = new List<string>();
            DataTable t4 = db.NumSurvExp();
            if (t4 != null)
            {
                for (int i = 0; i < t4.Rows.Count; i++)
                {
                    l4.Add(Convert.ToString(t4.Rows[i][0]));
                }
                for (int i = 0; i < t4.Rows.Count; i++)
                {
                    d4.Add(Convert.ToString(t4.Rows[i][1]));
                }

                ViewBag.l4 = l4;
                ViewBag.d4 = d4;
            }

            List<string> l5 = new List<string>();
            List<string> d5 = new List<string>();
            DataTable t5 = db.EventsRevenue();
            if (t5 != null)
            {
                for (int i = 0; i < t5.Rows.Count; i++)
                {
                    l5.Add(Convert.ToString(t5.Rows[i][0]));
                }
                for (int i = 0; i < t5.Rows.Count; i++)
                {
                    d5.Add(Convert.ToString(t5.Rows[i][1]));
                }

                ViewBag.l5 = l5;
                ViewBag.d5 = d5;
            }

            List<string> l6 = new List<string>();
            List<string> d6 = new List<string>();
            DataTable t6 = db.NumInvitedArtist();
            if (t6 != null)
            {
                for (int i = 0; i < t6.Rows.Count; i++)
                {
                    l6.Add(Convert.ToString(t6.Rows[i][0]));
                }
                for (int i = 0; i < t6.Rows.Count; i++)
                {
                    d6.Add(Convert.ToString(t6.Rows[i][1]));
                }

                ViewBag.l6 = l6;
                ViewBag.d6 = d6;
            }
            List<string> l7 = new List<string>();
            List<string> d7 = new List<string>();
            DataTable t7 = db.CATEGORY_PRICE();
            if (t7 != null)
            {
                for (int i = 0; i < t7.Rows.Count; i++)
                {
                    l7.Add(Convert.ToString(t7.Rows[i][0]));
                }
                for (int i = 0; i < t7.Rows.Count; i++)
                {
                    d7.Add(Convert.ToString(t7.Rows[i][1]));
                }

                ViewBag.l7 = l7;
                ViewBag.d7 = d7;
            }
            return View();
            
        }


        public ActionResult AddAdmin()
        {
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin a)
        {
            if (ModelState.IsValid)
            {
                if (db.AddAdmin(a))
                    return RedirectToAction("Index");
                else
                    return HttpNotFound();
            }

            return View(a);
        }
        public ActionResult Orders(string Orderby, int? page, string sortdir)
        {
            if (Request.QueryString["table_search"].IsNullOrWhiteSpace() ||
                !Request.QueryString["table_search"].IsInt())
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
                admin.Orders = db.GetOrderById(Convert.ToInt32(Request.QueryString["table_search"]))
                    .ToPagedList(page ?? 1, 5);
                return View(admin);
            }
        }

        public ActionResult OrderDetails(int? orderid)
        {
            OrderInfoViewModel o = new OrderInfoViewModel();
            o.Companies = db.GetCompanies();
            if (orderid.HasValue)
                o.OrderInfo = db.GetOrderInfo((int) orderid);
            else
            {
                RedirectToAction("Orders");
            }
            o.Order = db.GetOrderById((int)orderid)[0];
            o.Artwork = db.GetArtworkWithCode(o.OrderInfo.AW_CODE);
            return View(o);
        }
        [HttpPost]
        public ActionResult OrderDetails(OrderInfoViewModel o)
        {
            if(ModelState.IsValid)
            {
                o.OrderInfo.ADMIN_ID = Convert.ToInt32(User.Identity.Name); 
                if (db.AssignOrder(o.OrderInfo))
                    return View("Index");

            }
            return HttpNotFound();
        }

        public ActionResult Reports(string Orderby, int? page, string sortdir)
        {
            if (Request.QueryString["table_search"].IsNullOrWhiteSpace() ||
                !Request.QueryString["table_search"].IsInt())
            {
                sortdir = sortdir == "desc" ? "asc" : "desc";
                bool asc = sortdir == "desc" ? true : false;
                if (Orderby == "USER_NAME")
                {
                    admin.Reports = db.GetSortedReports("USER_NAME", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else if (Orderby == "ORDER_ID")
                {
                    admin.Reports = db.GetSortedReports("ORDER_ID", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else
                {
                    admin.Reports = db.GetSortedReports("ADMIN_ID", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
            }
            else
            {
                admin.Reports = db.GetReportById(Convert.ToInt32(Request.QueryString["table_search"]))
                    .ToPagedList(page ?? 1, 5);
                return View(admin);
            }
        }

        public ActionResult ReportAction(int? reportid)
        {
            ReportViewModel r = new ReportViewModel();
            if (reportid.HasValue)
            {
                r.Report = db.GetReportById((int)reportid)[0];
                r.OrderInfo = db.GetOrderInfo(r.Report.ORDER_ID);
                r.Order = db.GetOrderById(r.Report.ORDER_ID)[0];
                r.Adminid = Convert.ToInt32(User.Identity.Name); 
            }
            else
            {
                RedirectToAction("Reports");
            }

            return View(r);
        }

        [HttpPost]
        public ActionResult ReportAction()
        {
            Report r = new Report();
            r.ADMIN_ID = Convert.ToInt32(User.Identity.Name);
            r.REPORT_ID = (int)Session["reportid"];
            if (ModelState.IsValid)
                db.SolveReport(r);
            return RedirectToAction("Reports");
        }

        public ActionResult Proposals(string Orderby, int? page, string sortdir)
        {
            if (Request.QueryString["table_search"].IsNullOrWhiteSpace())
            {
                sortdir = sortdir == "desc" ? "asc" : "desc";
                bool asc = sortdir == "desc" ? true : false;
                if (Orderby == "Artist")
                {
                    admin.Artworks = db.GetSortedProposedArtworks("ARTIST_UNAME", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else if (Orderby == "Category")
                {
                    admin.Artworks = db.GetSortedProposedArtworks("CATEGORY_NAME", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else if (Orderby == "Title")
                {
                    admin.Artworks = db.GetSortedProposedArtworks("TITLE", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else if (Orderby == "Price")
                {
                    admin.Artworks = db.GetSortedProposedArtworks("Price", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
                else
                {
                    admin.Artworks = db.GetSortedProposedArtworks("AW_CODE", asc).ToPagedList(page ?? 1, 5);
                    return View(admin);
                }
            }
            else
            {
                admin.Artworks = db.GetProposedArtworksByArtist(Request.QueryString["table_search"])
                    .ToPagedList(page ?? 1, 5);
                return View(admin);
            }
        }

        public ActionResult ProposalsAction(int? code)
        {
            if (code == null)
                return HttpNotFound();
            admin.Artwork = db.GetArtworkWithCode((int) code);
            if (admin.Artwork.ADMIN_ID != null)
                return HttpNotFound();
            else
                return View(admin);
        }

        [HttpPost]
        public ActionResult ProposalsAction(string code)
        {
            if (Request.Form["Approve"] != null)
            {
                
                if (!db.ApproveArtwork(Convert.ToInt32(User.Identity.Name), Convert.ToInt32(code), 1))
                    return HttpNotFound();

            }
            else
            {
                if (!db.ApproveArtwork(Convert.ToInt32(User.Identity.Name), Convert.ToInt32(code), 0))
                    return HttpNotFound();

            }

            return RedirectToAction("Proposals");
        }

        public ActionResult Event()
        {
            //temporary

            return View();
        }

        [HttpPost]
        public ActionResult Event(Event e)
        {
            if (ModelState.IsValid)
            {
                //temporary for testing will not be hardcoded later
                e.SelectedArtists = (string[])Session["selected"];
                e.ADMIN_ID = Convert.ToInt32(User.Identity.Name) ;
                if (e.imagefile != null)
                {
                    string imagefilename = Path.GetFileNameWithoutExtension(e.imagefile.FileName);
                    string extension = Path.GetExtension(e.imagefile.FileName);
                    imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                    e.imagefile.SaveAs(Path.Combine(Server.MapPath("~/Images/"), imagefilename));
                    e.IMAGE = "~/Images/" + imagefilename;
                }
                else
                {
                    e.IMAGE = "~/Images/defaul.png";
                }

                if (db.CreateEvent(e) && db.InviteArtist(e.TITLE, e.SelectedArtists))
                    return RedirectToAction("Index");
                else
                    return HttpNotFound();
            }
            else
            {
                return View(e);
            }

        }

        public ActionResult EditEvent(string EventTitle)
        {

            Event e = db.GetEventInfo(EventTitle)[0];
            return View(e);
        }

        [HttpPost]
        [ActionName("EditEvent")]
        public ActionResult editEvent(Event e, string old)
        {

            if (ModelState.IsValid)
            {
                
                e.ADMIN_ID = Convert.ToInt32(User.Identity.Name);
                if (e.imagefile != null)
                {
                    string imagefilename = Path.GetFileNameWithoutExtension(e.imagefile.FileName);
                    string extension = Path.GetExtension(e.imagefile.FileName);
                    imagefilename = imagefilename + DateTime.Now.ToString("yymmssfff") + extension;
                    e.imagefile.SaveAs(Path.Combine(Server.MapPath("~/Images/"), imagefilename));
                    e.IMAGE = "~/Images/" + imagefilename;
                }
                else
                {
                    e.IMAGE = "~/Images/defaul.png";
                }

                if (db.EditEvent(e, old))
                    return RedirectToAction("Index");
                else
                    return HttpNotFound();
            }
            else
            {
                return View(e);
            }

        }

        public ActionResult ViewEvents(int? page)
        {
            //some dummy user name as events attended by user h will not be viewed
            IPagedList<Event> events = db.GetEvents("h").ToPagedList(page ?? 1, 5);
            return View(events);

        }

        public ActionResult LoadData()
        {
            var data = db.GetArtists();
            return Json(new{data = data}, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveSelected(string[] list)
        {
            Session["selected"] = list;
            return Json(TempData["selected"]);
        }

        public ActionResult ShippingCompany()
        {
            return View();
        }

        public ActionResult LoadShipping()
        {
            var data = db.GetCompanies();
            return Json(new {data = data}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult deleteCompany(string[] list)
        {
            if(list != null)
                foreach (var company in list)
                {
                    db.deleteCompany(company);
                }
            return RedirectToAction("ShippingCompany");
        }

        [HttpPost]
        public ActionResult ShippingCompany(ShippingCompany s)
        {

            if (db.Addcompany(s))
                return RedirectToAction("ShippingCompany");
            else
                return HttpNotFound();

        }
    }
}