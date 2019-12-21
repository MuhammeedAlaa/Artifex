using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Event
    {
        [DisplayName("Title")]
        [Required(ErrorMessage = "Please enter the title")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string TITLE { get; set; }
        public int ADMIN_ID { get; set; }
        [DisplayName("Event Image")]
        public string IMAGE { get; set; }
        [DisplayName("Event Image")]
        public HttpPostedFileBase imagefile { get; set; }
        [DisplayName("Price")]
        [Required(ErrorMessage = "Please enter the price of ticket ")]
        [Range(20, 1000,ErrorMessage ="Please enter a price from 20 to 1000")]
        public int TICKET_PRICE { get; set; }

        [DisplayName("Date")]
        [Required]
        public DateTime EVENTDATE { get; set; }

        [DisplayName("Location")]
        [Required(ErrorMessage = "Please entet the location")]
        [StringLength(20, ErrorMessage = "Max length = 20 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string LOCATION { get; set; }

        [DisplayName("Number of Tickets")]
        [Required(ErrorMessage = "Please enter the number of tickets ")]
        [Range(10, 1000)]
        public int TICKETS_NUM { get; set; }

        [DisplayName("Info")]
        [Required(ErrorMessage = "Please entet the location")]
        [StringLength(200, ErrorMessage = "Max length = 200 characters")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Only letters are allowed")]
        public string INFO { get; set; }

        public string[] SelectedArtists { get; set; }
    }
}