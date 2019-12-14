using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "please enter your email correctly")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string email { get; set; }

        [Required(ErrorMessage ="please enter your password correctly")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Invalid password format")]
        public string password { get; set; }



    }
}