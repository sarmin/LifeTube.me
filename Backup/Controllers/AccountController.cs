using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Configuration;

using LifeTube.Models;

namespace LifeTube.Controllers
{
    public class AccountController : Controller
    {
        LifeTubeEntities1 db = new LifeTubeEntities1();
       
        
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
                        GlobalVariables.UserName=(from u in db.Users
                                                     where u.userid==GlobalVariables.UserId
                                                      select u.firstName).Single().ToString();
                        GlobalVariables.eMail = (from u in db.Users
                                                    where u.userid == GlobalVariables.UserId
                                                    select u.email).Single().ToString();  
                        return RedirectToAction("Index", "Home");                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");    
                }
            }
            return View(model);
        }
        public ActionResult _LogOnPartial()
        {
            var uName=(from u in db.Users
                       from p in db.Profiles
                           where u.userid==GlobalVariables.UserId
                           where u.profileId==p.profileId
                           select p.firstName).ToString();

            return PartialView("_LogOnPartial", new { Username = uName });
        }
        public ActionResult LogOff()
        {
            HttpContext.Session.Add("LifeTubeUser", 0);
            GlobalVariables.UserId = 0;
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        ////
        //// POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            User usr = new User();

            var uNameValidity = (from u in db.Users
                                 select u.email).ToArray();
            for (int i = 0; i < uNameValidity.Length; i++)
            {
                if (uNameValidity[i].Equals(model.Email))
                {
                    GlobalVariables.isValidUserName = false;
                    TempData["message"] = "An account already exist for this e-mail. please try with another e-mail";

                    break;
                }
                else
                {
                    GlobalVariables.isValidUserName = true;
                }

            }
            if (GlobalVariables.isValidUserName == true)
            {
                if (ModelState.IsValid)
                {
                    Profile p = new Profile();

                    p.firstName = model.firstName;
                    p.lastName = model.lastName;
                    p.createdOn = DateTime.Now;
                    p.email = model.Email;

                    db.Profiles.AddObject(p);
                    db.SaveChanges();

                    int pid=(from pr in db.Profiles
                                 where pr.email.Equals(model.Email)
                                 select pr.profileId).Single();

                    usr.email = model.Email;
                    usr.password = model.Password;
                    usr.firstName = model.firstName;
                    usr.lastName = model.lastName;
                    usr.phone = model.phone;
                    usr.rootUser = false;
                    usr.profileId = pid;
                    usr.createdDate = DateTime.Now;
                    usr.lastModifiedDate = DateTime.Now;

                    db.Users.AddObject(usr);
                    db.SaveChanges();
                    // db.Refresh();

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("sarmin@mtxbd.com");
                    message.To.Add(new MailAddress(usr.email));
                    message.Subject = " Welcome to LifeTube.me";
                    message.Body = "Dear " + usr.firstName + " " + usr.lastName + "," + Environment.NewLine + "Thank you for registering with LifeTube.me" + Environment.NewLine + "Username: " + usr.email + Environment.NewLine + "Password: " + usr.password + Environment.NewLine + "Regards," + Environment.NewLine + "LifeTube Admin";

                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                }

                return RedirectToAction("LogOn", "Account");

            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
            
        }
        ///////////////////////////End Registration Process//////////////////////////////

        [HttpGet]
        public ActionResult SentForgotPassworLink()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SentForgotPassworLink(SentForgotPasswordLinkModel model)
        {
            User usr = new User();

            var allEmail=(from u in db.Users
                          select u.email).ToArray();

            for (int i = 0; i < allEmail.Length; i++)
            {
                if (allEmail[i].Equals(model.Email))
                {
                    int uId = (from u in db.Users
                               where u.email.Equals(model.Email)
                               select u.userid).Single();
                    usr = db.Users.Single(u => u.userid == uId);
                    usr.resetPin = GenerateResetPin().ToString();
                    db.SaveChanges();

                    var resetPinToSendMail = (from u in db.Users
                                              where u.userid == uId
                                              select u.resetPin).Single();
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("sarmin@mtxbd.com");
                    message.To.Add(new MailAddress(model.Email));
                    message.Subject = " Set up a new password";
                    message.Body = "Please click the following link to reset your password." + Environment.NewLine + "Your reset PIN is: " + resetPinToSendMail.ToString() + Environment.NewLine + "http://lifetube.youpixel.com/Account/ResetPassword";

                    SmtpClient client = new SmtpClient();
                    client.Send(message);

                    TempData["message"] = "A link has been sent to your e-mail. Please check your mail";
                    break;
                }
                else
                {
                    TempData["message"] = "This e-mail doesn't exist in LifeTube database. Please try again";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            
                User usr = new User();
                int uId = (from u in db.Users
                                where u.email.Equals(model.Email)
                                select u.userid).Single();

                 var pin=(from u in db.Users
                              where u.userid==uId
                              select u.resetPin).Single();
                 if (model.resetPin.Equals(pin.ToString()))
                 {

                     usr = db.Users.Single(u => u.userid == uId);
                     usr.resetPin = "";
                     usr.password = model.NewPassword;
                     usr.lastModifiedBy = uId;
                     usr.lastModifiedDate = DateTime.Now;
                     db.SaveChanges();

                     TempData["message"] = "Password updated";
                 }
                 else
                 {
                     TempData["message"] = "You entered incorrect PIN. Please check your e-mail";
                 }
	
            //return RedirectToAction("LogOn","Account");
            return View();
           
        }
        public string GenerateResetPin()
	    {	   
	        string PinLength = "12";
	   
	        string ResetPin = "";
	 
	        string allowedChars = "";          
	        allowedChars = "1,2,3,4,5,6,7,8,9,0";  	    
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
	        allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
	        allowedChars += "~,!,@,#,$,%,^,&,*,+,?";
	 
	        char[] sep = { ',' };
	        string[] arr = allowedChars.Split(sep);
	 
	        string IDString = "";
	        string temp = "";
	  
	        Random rand = new Random();
	 
	        for (int i = 0; i < Convert.ToInt32(PinLength); i++)
	        {
	            temp = arr[rand.Next(0, arr.Length)];
	            IDString += temp;
	            ResetPin = IDString;	 
	        }	 
	        return ResetPin;
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

        //[HttpPost]
        //public JsonResult Search(string searchstring)
        //{
        //    //var suggestions = db.Users
        //    //    .Where(u => u.firstName.Contains(searchstring))
        //    //    .Select(u => new { u.firstName }).ToArray();
        //    //return Json(suggestions, JsonRequestBehavior.AllowGet);


        //    var suggestions = (from u in db.Users select u.firstName);

        //    var namelist = suggestions.Where(n => n.ToLower().StartsWith(searchstring.ToLower()));

        //    return Json(namelist, JsonRequestBehavior.AllowGet);

        //}
        //[HttpPost]
        //public JsonResult SearchUserNames(string searchString, int maxResult)
        //{
        //    UsersRepository repository = new UsersRepository();
        //    var results = repository.FindPeople(searchString, maxResult);
        //    //return Json(results);
        //    return Json(results, JsonRequestBehavior.AllowGet);

        //}

           
    }
    
}
