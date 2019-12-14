using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class BillingInfo
    {
        public string CARD_NUM { get; set; }
        public string USER_NAME { get; set; }
        public string STREET { get; set; }
        public string CITY { get; set; }
        public string CARD_HOLDER_NAME { get; set; }
        public int CVV { get; set; }
        public DateTime EXPIRY_DATE { get; set; }
    }
}