using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Please enter your report text")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string TEXT { get; set; }
    }
}