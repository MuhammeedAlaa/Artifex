using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGallery.Models;
using PagedList;

namespace ArtGallery.ViewModels
{
    public class AdminViewModel 
    {
        public Admin Admin = new Admin();
        public IPagedList<Order> Orders;
        public IPagedList<Report> Reports;
        public IPagedList<Artwork> Artworks;
    }

    
}