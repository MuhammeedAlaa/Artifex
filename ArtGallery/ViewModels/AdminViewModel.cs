using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGallery.Models;

namespace ArtGallery.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        public Admin Admin = new Admin();
    }

    public abstract class ViewModelBase
    {
    }
}