using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.ViewModels
{
    public class CustomOrderUserViewModel
    {
        public string  Category { get; set; }
        public int? ADMIN_ID { get; set; }
        public string TITLE { get; set; }
        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }
        public int DEPTH { get; set; }
        public string MATERIAL { get; set; }
        public string MEDIUM { get; set; }
        public int  Budget { get; set; }
        public DateTime Deadline { get; set; }

    }
}