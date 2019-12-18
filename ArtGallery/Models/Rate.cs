using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class RateArtwork
    {
        public string USER_NAME { get; set; }
        public int AW_CODE { get; set; }

        [Required(ErrorMessage = "Please enter the rating ")]
        [RegularExpression(" ^[0 - 5]$", ErrorMessage = "Enter a number between 0 and 5 ")]
        public int RATING { get; set; }
    }
}