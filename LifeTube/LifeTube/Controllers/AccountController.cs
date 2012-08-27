using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using  System.Net;
using Newtonsoft.Json.Linq;
using LifeTube.Models;

namespace LifeTube.Controllers
{
    public class AccountController : Controller
    {

       // [HttpGet]
       //// public ActionResult FacebookLogin(string token)
       // public ActionResult FacebookLogin()
       // {
       // //    System.Net.WebClient client = new System.Net.WebClient();
       // //    string JsonResult = client.DownloadString(string.Concat(
       // //           "https://graph.facebook.com/me?access_token=", token));
       // //    // Json.Net is really helpful if you have to deal
       // //    // with Json from .Net http://json.codeplex.com/
       // //    JObject jsonUserInfo = JObject.Parse(JsonResult);
       // //    // you can get more user's info here. Please refer to:
       // //    //     http://developers.facebook.com/docs/reference/api/user/
       // //    string username = jsonUserInfo.Value<string>("username");
       // //    string email = jsonUserInfo.Value<string>("email");
       // //    string locale = jsonUserInfo.Value<string>("locale");
       // //    string facebook_userID = jsonUserInfo.Value<string>("id");

       // //    // store user's information here...
       // //    FormsAuthentication.SetAuthCookie(username, true);
       //    // return RedirectToAction("Index", "Home");
       //     return View();
       // }
        public ActionResult LogOn()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var UserLogin = new DB_46167_lifetubeEntities();

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
                        _LogOnPartial();
                        return RedirectToAction("Profile", "Profile");
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");

                }
            }
            return View(model);
        }

        //
        // GET: /Account/LogOff
        public ActionResult _LogOnPartial()
        {
            var loggedInUser = new DB_46167_lifetubeEntities();
            var username = from u in loggedInUser.Users
                           where u.userid.Equals(Request.Cookies["LifeTubeUser"].Value)
                           select u.email.ToString();

            return PartialView("_LogOnPartial", new { Username = username });
        }


        public ActionResult LogOff()
        {
            HttpContext.Session.Add("LifeTubeUser", 0);
            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //DB_46167_lifetubeEntities register = new DB_46167_lifetubeEntities();
                //User usr = new User();

                //usr.email = model.Email;
                //usr.password = model.Password;
                //usr.firstName = model.firstName;

              //  var email = model.Email;
              //  var password = model.Password;
              //  User newUser = new Models.User();
              //  //Users newUser = new Users();
              //  newUser.email = email;
              //  //newUser.email = email;
              //  newUser.password = password;

              //  DB_46167_lifetubeEntities register = new DB_46167_lifetubeEntities();
              ////  MatrixEntities register = new MatrixEntities();
              // register.Use
              //  //register.Users.Add(newUser);
              //  register.SaveChanges();
            }

            //// If we got this far, something failed, redisplay form
            return View("Home", "Index", "Home");
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
