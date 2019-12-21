using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models 
{
    public class ExpertQualifications
    {
        public string EXPERT_UNAME { get; set; }

        [Required(ErrorMessage = "Please enter your qualifactions")]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string QUALIFICATIONS { get; set; }
    }
}