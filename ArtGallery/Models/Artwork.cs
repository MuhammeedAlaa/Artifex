using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Artwork
    {
        public int AW_CODE { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string ARTIST_UNAME { get; set; }
        public int? ADMIN_ID { get; set; }
        public string TITLE { get; set; }
        public bool ACCEPTED { get; set; }
        public bool PRIVACY { get; set; }
        public bool STATUS { get; set; }
        public string DESCRIPTION { get; set; }
        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }
        public int DEPTH { get; set; }
        public int PRICE { get; set; }
        public string MATERIAL { get; set; }
        public string MEDIUM { get; set; }
        public string SUBJECT { get; set; }
        public int YEAR { get; set; }
        
        public string PHOTO { get; set; }
        [Required]
        public HttpPostedFileBase imagefile { get; set; }
    }
}