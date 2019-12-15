using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Admin
    {

        public int ADMIN_ID { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public int SALARY { get; set; }
        public string Name { get; set; }
        [Column("PROFILE_PICTURE")]
        public string ProfilePic { get; set; }
    }
}