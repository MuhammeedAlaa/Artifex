using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Report
    {
        public int REPORT_ID { get; set; }
        public string USER_NAME { get; set; }
        public int? ADMIN_ID { get; set; }
        public int ORDER_ID { get; set; }
        public string TEXT { get; set; }
    }
}