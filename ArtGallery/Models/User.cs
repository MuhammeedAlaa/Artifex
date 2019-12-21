using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class User
    {
       
        [Required(ErrorMessage = "Please enter your user name")]
        [RegularExpression("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Please enter a valid user name")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "please enter your email correctly")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Invalid password format your password should have upper and lower case and a number or a specail character")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string FNAME { get; set; }

        [Required(ErrorMessage = "Please enter your middle name")]
        [StringLength(1, ErrorMessage = "Max length = 1 characters")]
        [RegularExpression("^[a-zA-Z]", ErrorMessage = "Please enter a valid minit it is one char and only letters are allowed")]
        public string MINIT { get; set; }


        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string LNAME { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression("01[0-9]{9}", ErrorMessage = "The phone number must be in format 01020809076")]
        public string PHONE { get; set; }
        public string PROFILE_PIC { get; set; }
        public HttpPostedFileBase imagefile { get; set; }
    }
}


