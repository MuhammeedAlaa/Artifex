using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Report
    {
        [Required]
        public int REPORT_ID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string USER_NAME { get; set; }
        public int? ADMIN_ID { get; set; }
        [Required]
        public int ORDER_ID { get; set; }

        [Required(ErrorMessage = "Please enter your report text")]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string TEXT { get; set; }
    }
}