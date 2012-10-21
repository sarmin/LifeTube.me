using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LifeTube.Models
{
    public class UsersModel
    {
        public int userId { get; set; }

        [DataType(DataType.EmailAddress)]
        //[Email]
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public int profileid { get; set; }
        public Boolean rootUser { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int lastModifiedBy { get; set; }
        public DateTime lastModifiedDate { get; set; }

        [Compare("Password")]
        public string PasswordConfirm { get; set; }


        
    }
}