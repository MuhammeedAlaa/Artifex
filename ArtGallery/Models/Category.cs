﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Please enter the category")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string NAME { get; set; }
    }
}