﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ArtGallery.ViewModels
{
    public class ExpertViewModel
    {
        public string EXPERT_UNAME { get; set; }
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]

        public string BIO { get; set; }


        [Range(1940, 1995)]
        public int? BYEAR { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]

        public string QUALIFICATIONS { get; set; }
    }
}