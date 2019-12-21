using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Order
    {
        public int ORDER_ID { get; set; }

        
        [Required(ErrorMessage = "Please enter the status")]
        [RegularExpression("^(true|false)$", ErrorMessage = "Please enter a valid user name")]
        public bool STATUS { get; set; }
        public DateTime ORDER_DATE { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
    }
}