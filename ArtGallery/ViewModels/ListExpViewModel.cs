using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGallery.Models;
using PagedList;

namespace ArtGallery.ViewModels
{
    public class ListExpViewModel
    {
        public IPagedList<Expert> Experts;
        public List<string> Emails;
    }
}