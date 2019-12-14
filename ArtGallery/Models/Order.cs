using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Order
    {
        public int ORDER_ID { get; set; }
        public bool STATUS { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
    }
}