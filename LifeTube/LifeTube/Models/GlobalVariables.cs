using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace LifeTube.Models
{
    public static class GlobalVariables
    {
        public static SqlCommand conn { get; set; }
        public static int UserId { get; set; }
        public static int RootUser { get; set; }
        public static string RootUserHierarchyID { get; set; }
        public static String UserName { get; set; }
        public static DB_46167_lifetubeEntities db { get; set; }
    }
}