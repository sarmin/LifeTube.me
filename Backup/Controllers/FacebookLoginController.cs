using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Net;
using Facebook.Web;

using System.Web.Security;
using System.Web.Routing;
using NotificationsExtensions;
using NotificationsExtensions.ToastContent;

namespace LifeTube.Controllers
{
    public class FacebookLoginController : Controller
    {
        //
        // GET: /FacebookLogin/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        // public ActionResult FacebookLogin(string token)
        public ActionResult FacebookLogin()
        {
            if (Facebook.Web.FacebookWebContext.Current.IsAuthenticated())
            {
                return RedirectToAction("Profile", "Home");
            }
            return View();
        }
        public ActionResult FacebookLogof()
        {
            WnsAccessTokenProvider accessToken = new WnsAccessTokenProvider("435567116484698", "ca5f010bb5a0ccd080cb2d47c6d41f88");
            //WnsAccessTokenProvider accessToken = new WnsAccessTokenProvider(435567116484698,"ca5f010bb5a0ccd080cb2d47c6d");
            string logoutUrl = String.Format(" facebook.com/logout.php?access_token={0}&next={1}", accessToken, Url.Action("Index", "Support")); 
            
            return Redirect(logoutUrl);
            
            
            //return Redirect("https://www.facebook.com/logout.php?"
            //      + "next=" + facebookLogoffUrl + "&access_token=" + accessToken);

        }

    }
}
