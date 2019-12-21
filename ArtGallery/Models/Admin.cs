using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ArtGallery.Models
{
    public class Admin
    {

        public int ADMIN_ID { get; set; }

        [Required(ErrorMessage = "please enter your email correctly")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail address")]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Invalid password format")]
        public string PASSWORD { get; set; }


        [Required(ErrorMessage = "Please enter the salary ")]
        [Range(10, 5000)]
        public int SALARY { get; set; }

    }
}