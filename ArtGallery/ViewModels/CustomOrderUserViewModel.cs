using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.ViewModels
{
    public class CustomOrderUserViewModel
    {
        [Required(ErrorMessage = "Please entet the description ")]
        [StringLength(100, ErrorMessage = "Max length = 100 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string DESCRIPTION { get; set; }
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