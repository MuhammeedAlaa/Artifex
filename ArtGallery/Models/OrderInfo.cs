using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class OrderInfo
    {
        public int ORDER_ID { get; set; }
        public int ADMIN_ID { get; set; }
        public string SHIPPING_NAME { get; set; }
        public string USER_NAME { get; set; }
        public int AW_CODE { get; set; }
    }
}