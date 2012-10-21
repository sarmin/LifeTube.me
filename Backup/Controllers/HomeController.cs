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
        LifeTubeEntities1 db = new LifeTubeEntities1();
        [HttpGet]
        public ActionResult Index()
        {
           

            //if (!string.IsNullOrEmpty(Request.Params["signed_request"]))
            //{
            //    ViewBag.IsAuthenticated = true;
            //    Dictionary<string, string> info = Utility.DecodePayload(Request.Params["signed_request"]);
            //    Session["access_token"] = info;
            //    Facebook.FacebookAPI api = new Facebook.FacebookAPI(info["oauth_token"]);
            //    var json = api.Get("/me");
            //}
            //else
            //    ViewBag.IsAuthenticated = false;
            //return View();
            return View();
        }
        [HttpPost]
        public ActionResult Index(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var UserLogin = new LifeTubeEntities1();

                var user = from u in UserLogin.Users
                           where u.email.Equals(model.UserName)
                           where u.password.Equals(model.Password)
                           select u;
                if (user.FirstOrDefault() != null)
                {
                    HttpContext.Session.Add("LifeTubeUser", user.FirstOrDefault().userid);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        GlobalVariables.UserId = (from u in db.Users
                                                  where u.email == model.UserName
                                                  select u.userid).Single();
                        GlobalVariables.UserName = (from u in db.Users
                                                    where u.userid == GlobalVariables.UserId
                                                    select u.firstName).Single().ToString();
                        GlobalVariables.eMail = (from u in db.Users
                                                 where u.userid == GlobalVariables.UserId
                                                 select u.email).Single().ToString();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
           
            return View(model);
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
