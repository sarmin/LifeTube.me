using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace LifeTube.Models
{
    public class UserCircleModel
    {
        public int userCircleId { get; set; }
        public int userFrom { get; set; }
        public int userTo { get; set; }
        public string relationship { get; set; }
       
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public int lastModifiedBy { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public Boolean isAccepted { get; set; }
        
        public string firstName{ get; set; }
        public UsersModel UsersModel { get; set; }
        public Profile profileModel { get; set; }
   
    }
    public class GetUserModel
    {
        public int userid { get; set; }
        public string firstName { get; set; }
        public string image { get; set; }
    }
    public class AddToCircleModel
    {
        public int profileId { get; set; }
        public int userCircleId { get; set; }
        public int userId { get; set; }
        public int userFrom { get;set;}
        public int userTo{get;set;}
        public Boolean isAccepted { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public string relationship { get; set; }
    }
    public class UsersAndCircleModel
    {
        public UsersModel UsersModel { get; set; }
        public UserCircleModel UserCircleModel { get; set; }
    }
}