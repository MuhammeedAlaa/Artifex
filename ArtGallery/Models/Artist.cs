using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Artist
    {
        [Required(ErrorMessage = "Please enter your user name")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Please enter a valid user name")]
        public string ARTIST_UNAME { get; set; }

        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string BIO { get; set; }

        [Range(1931 , 1999 )]
        public int? BYEAR { get; set; }

        [Required(ErrorMessage = "Please enter your start salary ")]
        public int START_SALARY { get; set; }

        [Required(ErrorMessage = "Please enter your end salary ")]
        public int END_SALARY { get; set; }

        public string Selected { get; set; }
    }
}