using ArtGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.ViewModels
{
    public class ScoreBoardViewModel
    {
        public string artist { get; set; }
        public int rate { get; set; }
        public string path { get; set; }
        public string Email { get; set; }
    }
}