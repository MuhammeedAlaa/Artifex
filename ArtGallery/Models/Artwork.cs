﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Artwork
    {
        public int AW_CODE { get; set; }

        [Required(ErrorMessage = "Please enter the category ")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string CATEGORY_NAME { get; set; }

        [Required(ErrorMessage = "Please enter your user name")]
        [RegularExpression("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Please enter a valid user name")]
        public string ARTIST_UNAME { get; set; }

       
        public int? ADMIN_ID { get; set; }

        [Required(ErrorMessage = "Please enter the tittle ")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string TITLE { get; set; }
        public bool ACCEPTED { get; set; }

        [Required(ErrorMessage = "Please enter the privacy")]
        [RegularExpression("^(true|false)$", ErrorMessage = "Please enter a valid user name")]
        public bool PRIVACY { get; set; }
  
        [Required]
        public bool STATUS { get; set; }

        [Required(ErrorMessage = "Please entet the description ")]
        [StringLength(100, ErrorMessage = "Max length = 100 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string DESCRIPTION { get; set; }

        [Required(ErrorMessage ="Please enter the width ")]
        [Range(20, 200,ErrorMessage = "please enter number from 20 to 200")]
        public int WIDTH { get; set; }

        [Required(ErrorMessage = "Please enter the heigth ")]
        [Range(20, 200, ErrorMessage = "please enter number from 20 to 200")]
        public int HEIGHT { get; set; }

        [Required(ErrorMessage = "Please enter the depth ")]
        [Range(2, 15, ErrorMessage ="please enter number from 2 to 15")]
        public int DEPTH { get; set; }

        [Required(ErrorMessage = "Please enter the price ")]
        [Range(20, 10000, ErrorMessage = "please enter a price from 20 to 10000")]
        public int PRICE { get; set; }

        [Required(ErrorMessage = "Please enter the material ")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string MATERIAL { get; set; }

        [Required(ErrorMessage = "Please enter the medium ")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string MEDIUM { get; set; }

        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string SUBJECT { get; set; }
        [Range(1950,2020,ErrorMessage ="please enter a year from 190 to 2020")]
        public int YEAR { get; set; }
        
        public string PHOTO { get; set; }
        public HttpPostedFileBase imagefile { get; set; }
        public string Selected { get; set; }
        public string STS { get; set; }
    }
}