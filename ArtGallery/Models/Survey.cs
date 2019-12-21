using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Survey
    {

        public int SURVEY_ID { get; set; }
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        public string EXPERT_UNAME { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Max length = 50 characters")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Please enter your budget ")]
        [Range(100,100000000000,ErrorMessage ="Please enter number more than 100")]
        public int BUDGET { get; set; }
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        public string MORE_INFO { get; set; }
        public int? RATING { get; set; }

    }
}