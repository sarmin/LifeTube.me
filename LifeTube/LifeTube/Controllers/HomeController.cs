using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using LifeTube.Models;
using Facebook.Web.Mvc;


namespace LifeTube.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Welcome to LifeTube";
            
            //if (!string.IsNullOrEmpty(Request.Params["signed_request"]))
            //{
            //    ViewBag.IsAuthenticated =true;
            //    Dictionary<string,string> info = Utility.DecodePayload(Request.Params["signed_request"]);
            //    Session["access_token"] = info;
            //    Facebook.FacebookAPI api = new Facebook.FacebookAPI(info["oauth_token"]);
            //    var json = api.Get("/me");
            //}
            //else
            //  ViewBag.IsAuthenticated = false;
            //return View();
            return View();
        }
        [Facebook.Web.Mvc.FacebookAuthorize(LoginUrl = "/FacebookLogin/FacebookLogin")]
        public ActionResult Profile()
        {
            var client = new Facebook.Web.FacebookWebClient();

            dynamic me = client.Get("me");
            ViewBag.Name = me.name;
            ViewBag.Id = me.id;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
