using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Artist
    {
        public string ARTIST_UNAME { get; set; }
        public string BIO { get; set; }
        public int? BYEAR { get; set; }
        public int START_SALARY { get; set; }
        public int END_SALARY { get; set; }
    }
}