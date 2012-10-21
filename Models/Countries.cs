using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeTube.Models
{
   
    public class Countries
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
             


        internal static List<Countries> GetCountriesFlags(string searchString)
        {
            List<Countries> obj = new List<Countries>
            {
                 new Countries { Id = 1, Name = "India",Flag="/Flags/India.png" },
                            new Countries { Id = 2, Name = "Pakisthan",Flag="/Flags/Pakistan.png" },
                            new Countries { Id = 3, Name = "SriLanka",Flag="/Flags/SriLanka.png" },
                            new Countries { Id = 4, Name = "Bangladesh",Flag="/Flags/Bangladesh.png" },
                            new Countries { Id = 5, Name = "China",Flag="/Flags/China.png" },
                            new Countries { Id = 6, Name = "Russia",Flag="/Flags/RussianFederation.png" },
                            new Countries { Id = 7, Name = "USA" ,Flag="/Flags/USA.png"}
            };

           return obj.FindAll(delegate(Countries objc) { return objc.Name.ToLower().Contains(searchString.ToLower()); });
        }
    }   
}