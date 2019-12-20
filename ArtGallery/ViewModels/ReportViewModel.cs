using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGallery.Models;

namespace ArtGallery.ViewModels
{
    public class ReportViewModel
    {
        public int Adminid { get; set; }
        public Report Report { get; set; }
        public Order Order { set; get; }
        public OrderInfo OrderInfo { get; set; }
    }
}