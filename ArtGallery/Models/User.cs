using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class User
    {

        public string USER_NAME { get; set; }


        public string EMAIL { get; set; }


        public string PASSWORD { get; set; }

        public string FNAME { get; set; }

        public string MINIT { get; set; }



        public string LNAME { get; set; }

        public string PHONE { get; set; }
        public string PROFILE_PIC { get; set; }
        public HttpPostedFileBase imagefile { get; set; }
    }
}


