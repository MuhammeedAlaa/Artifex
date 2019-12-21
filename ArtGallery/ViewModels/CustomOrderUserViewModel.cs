using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.ViewModels
{
    public class CustomOrderUserViewModel
    {
        [Required(ErrorMessage = "Please entet the description ")]
        [StringLength(100, ErrorMessage = "Max length = 100 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string DESCRIPTION { get; set; }
        public string  Category { get; set; }
        public int? ADMIN_ID { get; set; }
        [Required(ErrorMessage = "Please enter the tittle ")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string TITLE { get; set; }
        [Required(ErrorMessage = "Please enter the width ")]
        [Range(20, 200, ErrorMessage = "please enter number from 20 to 200")]

        public int WIDTH { get; set; }
        [Required(ErrorMessage = "Please enter the heigth ")]
        [Range(20, 200, ErrorMessage = "please enter number from 20 to 200")]

        public int HEIGHT { get; set; }
        [Required(ErrorMessage = "Please enter the depth ")]
        [Range(2, 15, ErrorMessage = "please enter number from 2 to 15")]
        public int DEPTH { get; set; }
        [Required(ErrorMessage = "Please enter the material ")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]

        public string MATERIAL { get; set; }
        [Required(ErrorMessage = "Please enter the medium ")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]

        public string MEDIUM { get; set; }
        [Required(ErrorMessage = "Please enter your budget ")]
        [Range(20, 1000000, ErrorMessage = "please enter a number more than 20")]

        public int  Budget { get; set; }
        public DateTime Deadline { get; set; }

    }
}