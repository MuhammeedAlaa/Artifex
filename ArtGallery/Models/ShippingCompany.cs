﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class ShippingCompany
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter the shipping company name")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string NAME { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "please enter the email correctly")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [StringLength(50, ErrorMessage = "Max length = 50 characters")]
        [EmailAddress]
        public string EMAIL { get; set; }

        [DisplayName("Phone number")]
        [StringLength(13, ErrorMessage = "invalid format phone number should not excees 13 number")]
        [Required(ErrorMessage = "Please enter the phone number")]
        [RegularExpression("01[0-9]{9}", ErrorMessage = "The phone number must be in format 01020809076")]
        public string PHONE { get; set; }


        [DisplayName("Average Shipping fees")]
        [Required(ErrorMessage = "Please enter the shipping fees ")]
        [Range(20, 10000)]
        public int SHIPPING_FEES { get; set; }
    }
}