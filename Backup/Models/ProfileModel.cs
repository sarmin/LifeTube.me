using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace LifeTube.Models
{
    public class ProfileModel
    {
        public int profileId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string homeTown { get; set; }
        public string studiedAt { get; set; }
        public string workAt { get; set; }
        public string personalContact { get; set; }
        public string homeContact { get; set; }
        public string bloodGroup { get; set; }
        public string birthPlace { get; set; }
        public string hobby { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class CreateProfileModel
    {
        public int profileId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
       // public string country { get; set; }
        public IEnumerable<SelectListItem> country { get; set; }
        public string gender { get; set; }
        public string religion { get; set; }
        public string homeTown { get; set; }
        public string studiedAt { get; set; }
        public string workAt { get; set; }
        public string personalContact { get; set; }
        public string homeContact { get; set; }
        public string bloodGroup { get; set; }
        public string birthPlace { get; set; }
        public string hobby { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public DateTime createdOn { get; set; }
        
    }
    //public class EditProfileModel
    //{
    //    public int profileId { get; set; }
    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //    public DateTime dateOfBirth { get; set; }
    //    public string streetAddress { get; set; }
    //    public string city { get; set; }
    //    public string postalCode { get; set; }
    //    public string country { get; set; }
    //    public string gender { get; set; }
    //    public string religion { get; set; }
    //    public string homeTown { get; set; }
    //    public string studiedAt { get; set; }
    //    public string workAt { get; set; }
    //    public string personalContact { get; set; }
    //    public string homeContact { get; set; }
    //    public string bloodGroup { get; set; }
    //    public string birthPlace { get; set; }
    //    public string hobby { get; set; }
    //    public DateTime lastModifiedOn { get; set; }
    //}
}