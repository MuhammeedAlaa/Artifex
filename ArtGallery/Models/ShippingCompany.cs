using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class ShippingCompany
    {
        [Required(ErrorMessage = "Please enter the shipping company name")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string NAME { get; set; }

        [Required(ErrorMessage = "please enter the email correctly")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Please enter the phone number")]
        [RegularExpression("01[0-9]{9}", ErrorMessage = "The phone number must be in format 01020809076")]
        public string PHONE { get; set; }

        [Required(ErrorMessage = "Please enter the shipping fees ")]
        [RegularExpression(" ^[5 - 150]$", ErrorMessage = "Enter a number between 5 and 150 ")]
        public int SHIPPING_FEES { get; set; }
    }
}