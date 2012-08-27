using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using LifeTube.Models;
namespace LifeTube.Models
{
    //public class Utility
    //{
    //    //public static Dictionary<string, string> DecodePayload(string payload)
    //    //{
    //        //var parameters = new Dictionary<string, string>();

    //        //try
    //        //{
    //        //    string[] sB64String = payload.Split('.');
    //        //    payload = payload.Replace((sB64String[0] + "."), string.Empty);
    //        //    //End
    //        //    var encoding = new System.Text.UTF8Encoding();
    //        //    var decodedJson = payload.Replace("=", string.Empty).Replace('-', '+').Replace('_', '/');
    //        //    var base64JsonArray = Convert.FromBase64String(decodedJson.PadRight(decodedJson.Length + (4 -decodedJson.Length % 4) % 4, '='))

    //        //    var json = encoding.GetString(base64JsonArray);

    //        //    var jObject = JSONObject.CreateFromString(json);
    //        //    //parameters.Add("liked", jObject.Dictionary["page"].Dictionary["liked"].Boolean.ToString());
    //        //    {
    //        //        parameters.Add("isAuthorized", "true");
    //        //        parameters.Add("oauth_token", jObject.Dictionary["oauth_token"].String);
    //        //        parameters.Add("profile_id", jObject.Dictionary["user_id"].String);
    //        //    }
    //        //    else
    //        //    {
    //        //        parameters.Add("isAuthorized", "false");
    //        //        parameters.Add("oauth_token", string.Empty);
    //        //    }

    //        //}
    //        //catch { }

    //        //return parameters;
    //    //}
    //}
}