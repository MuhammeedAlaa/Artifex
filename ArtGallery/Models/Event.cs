using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Event
    {
        public string TITLE { get; set; }
        public int ADMIN_ID { get; set; }
        public string IMAGE { get; set; }
        public int TICKET_PRICE { get; set; }
        public DateTime EVENTDATE { get; set; }
        public string LOCATION { get; set; }
        public int TICKETS_NUM { get; set; }
        public string INFO { get; set; }
    }
}