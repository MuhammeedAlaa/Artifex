using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class BillingInfo
    {
        [Required]
        [StringLength(18, ErrorMessage = "Max length = 18 characters")]
        [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = "invalid card format")]
        public string CARD_NUM { get; set; }

        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
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

        [Required]
        [RegularExpression("^([0-9]{3}|[0-9]{4})$", ErrorMessage = "CVV must be of 3 to 4 digits only")]
        public int CVV { get; set; }

        [Required]
        public DateTime EXPIRY_DATE { get; set; }
    }
}