using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Expert
    {
        [Required(ErrorMessage = "Please enter your user name")]
        [RegularExpression("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Please enter a valid user name")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string EXPERT_UNAME { get; set; }

        [Required(ErrorMessage = "Please enter your BIO")]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string BIO { get; set; }
        [Range(1930,2000,ErrorMessage ="please enter a date from 1930 to 2000")]
        public int? BYEAR { get; set; }
    }
}