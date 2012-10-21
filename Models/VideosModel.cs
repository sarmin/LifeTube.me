using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeTube.Models
{
    public class VideosModel
    {
        public int videoid { get; set; }
        public string videoUrl { get; set; }
        public int userid { get; set; }
        public DateTime storedOn { get; set; }
        public string videoTitle { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string media { get; set; }
        public string keywords { get; set; }
        public Boolean youTubeEntry { get; set; }
    }
}