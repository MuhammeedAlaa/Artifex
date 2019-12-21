using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class OrderInfo
    {
        [Required]
        public int ORDER_ID { get; set; }
        [Required]
        public int? ADMIN_ID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string SHIPPING_NAME { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string USER_NAME { get; set; }
        public int AW_CODE { get; set; }
    }
}