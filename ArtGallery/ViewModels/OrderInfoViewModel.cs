using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGallery.Models;

namespace ArtGallery.ViewModels
{
    public class OrderInfoViewModel
    {
        public int Admin_id { set; get; }
        public Artwork Artwork { get; set; }
        public Order Order { get; set; }
        public List<ShippingCompany> Companies { set; get; }
        public OrderInfo OrderInfo { get; set; }
    }
}