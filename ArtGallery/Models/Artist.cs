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
        [RegularExpression("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Please enter a valid user name")]
        public string ARTIST_UNAME { get; set; }

        [Required(ErrorMessage = "Please enter your BIO")]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string BIO { get; set; }

        public int? BYEAR { get; set; }

        [Required(ErrorMessage = "Please enter your start salary ")]
        public int START_SALARY { get; set; }

        public int END_SALARY { get; set; }
    }
}