using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Survey
    {
        public int SURVEY_ID { get; set; }
        public string EXPERT_UNAME { get; set; }
        public string USER_NAME { get; set; }
        public int BUDGET { get; set; }
        public string ORIENTATION { get; set; }
        public string MORE_INFO { get; set; }
        public int? RATING { get; set; }

    }
}