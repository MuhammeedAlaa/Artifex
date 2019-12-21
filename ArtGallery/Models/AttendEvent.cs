using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ArtGallery.Models
{
    //this may be deleted
    public class AttendEvent
    {
        [Required]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string TITLE { set; get; }
        [Required]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string USER_NAME { set; get; }
    }
}