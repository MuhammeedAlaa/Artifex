using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class BillingInfo
    {

        public string CARD_NUM { get; set; }

    
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Please enter the street")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string STREET { get; set; }

        [Required(ErrorMessage = "Please enter the city")]
        [StringLength(10, ErrorMessage = "Max length = 10 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string CITY { get; set; }

        [Required(ErrorMessage = "Please enter the card holder name")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string CARD_HOLDER_NAME { get; set; }
        public int CVV { get; set; }
        public DateTime EXPIRY_DATE { get; set; }
    }
}