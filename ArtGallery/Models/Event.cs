using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Event
    {
        [Required(ErrorMessage = "Please entet the tittle")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string TITLE { get; set; }
        public int ADMIN_ID { get; set; }
        public string IMAGE { get; set; }
        public HttpPostedFileBase imagefile { get; set; }

        [Required(ErrorMessage = "Please enter the price of ticket ")]
        [RegularExpression(" ^[20 - 150]$", ErrorMessage = "Enter a number between 20 and 150 ")]
        public int TICKET_PRICE { get; set; }


        public DateTime EVENTDATE { get; set; }

        [Required(ErrorMessage = "Please entet the location")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string LOCATION { get; set; }

        [Required(ErrorMessage = "Please enter the number of tickets ")]
        [RegularExpression(" ^[10 - 150]$", ErrorMessage = "Enter a number between 10 and 150 ")]
        public int TICKETS_NUM { get; set; }

        [Required(ErrorMessage = "Please entet the location")]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string INFO { get; set; }
    }
}