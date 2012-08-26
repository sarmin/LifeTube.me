using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeTube.Models
{
    public class ProfileModel
    {
        public int profileid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime createdOn { get; set; }
        
    }
}