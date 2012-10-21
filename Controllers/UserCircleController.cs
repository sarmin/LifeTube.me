using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using LifeTube.Models;
using System.Collections;
using System.Reflection;

namespace LifeTube.Controllers
{
    public class UserCircleController : Controller
    {
        LifeTubeEntities1 db = new LifeTubeEntities1();

        public ViewResult MyCircle()  // GET: /UserCircle/
        {
            var myCircle = default(IList);

            var all = (from uc in db.UserCircles
                       where uc.userFrom == GlobalVariables.UserId || uc.userTo == GlobalVariables.UserId
                       where uc.isAccepted == true 
                       select new { uc.userFrom, uc.userTo }).ToArray();

            var f=(from uc in all select uc.userFrom).ToList();
            var t = (from uc in all select uc.userFrom).ToList();
           
           for (int i = 0; i < all.Length; i++)
			{
			    if(GlobalVariables.UserId==f[i])
                {
                    myCircle=(from usr in db.Users
                                    from uc in db.UserCircles
                                      where uc.userTo==usr.userid
                                      select new UserCircleModel()
                                       {                         
                                            firstName=usr.firstName,
                                            relationship=uc.relationship, 
                                                                      
                                        }).ToList();

                }
                else if (GlobalVariables.UserId == t[i])
                {
                    myCircle = (from usr in db.Users
                                    from uc in db.UserCircles
                                    where uc.userFrom == usr.userid
                                    select new UserCircleModel()
                                    {
                                        firstName = usr.firstName,
                                        relationship = uc.relationship,

                                    }).ToList();
                }
                else
                { } 
		    }
           ViewBag.FirstName = new SelectList(from u in db.Users select u).ToList();
           return View(myCircle);
            

        }
        public ActionResult UserCircle()
        {
            var myCircle = (from usr in db.Users
                            from uc in db.UserCircles
                            where uc.userFrom == GlobalVariables.UserId || uc.userTo == GlobalVariables.UserId
                            where uc.isAccepted == true
                            where usr.userid == uc.userFrom || usr.userid == uc.userTo
                            select new { usr.firstName, uc.relationship }).ToArray();                
            return View(myCircle);
        }
       
        [HttpPost]
        public JsonResult SearchUserNames(string searchString, int maxResult)
        {
            UsersRepository repository = new UsersRepository();
            var results = repository.FindPeople(searchString, maxResult);
        
            return Json(results, JsonRequestBehavior.AllowGet);

        }

        public ViewResult GetAllUsers()
        {
            var allUsrs = (from u in db.Users
                           select new UsersModel()
                           {
                               firstName = u.firstName,
                               userId=u.userid,
                           }).ToList();
            
            return View(allUsrs);
        }
        public ActionResult AddToCircle()
        {
            return View();
        }

        [HttpPost]
       [Button(Action="Add To Circle")]
        public ActionResult AddToCircle(UserCircleModel model)
        {
            try
            {
                //UserCircle uc = new UserCircle();
                            
                //uc.userFrom = GlobalVariables.UserId;
                //uc.createdOn = DateTime.Now;
                //uc.lastModifiedDate = DateTime.Now;
                //uc.lastModifiedBy = GlobalVariables.UserId;
                //uc.userTo =(from u in db.Users where u.profileId==GlobalVariables.ProfileIdForCircle select u.userid).Single();
                //uc.isAccepted = false;
                //uc.relationship = "Friend";

                //db.UserCircles.AddObject(uc);
                //db.SaveChanges();

                //TempData["message"] = "Add request has been sent";
                
                MailMessage message = new MailMessage();
                
                var toUsrEmail = (from u in db.Users where u.profileId == GlobalVariables.ProfileIdForCircle select u.email).ToString();
                var toUsrFname = from u in db.Users where u.profileId == GlobalVariables.ProfileIdForCircle select u.firstName;
                var toUsrLname = from u in db.Users where u.profileId == GlobalVariables.ProfileIdForCircle select u.lastName;

                var fromUsrFname = from u in db.Users where u.userid == GlobalVariables.UserId select u.firstName;
                var fromUsrLname = from u in db.Users where u.userid == GlobalVariables.UserId select u.lastName;

                message.From = new MailAddress("sarmin@mtxbd.com");
                message.To.Add(new MailAddress(toUsrEmail));
                message.Subject = " Welcome to LifeTube.me";
                message.Body = "Hey" + toUsrFname+ " " + toUsrLname + "," + Environment.NewLine + fromUsrFname+" "+fromUsrLname+ " Has added you to his/her circle on LifeTube";

                SmtpClient client = new SmtpClient();
                client.Send(message);
                
            }
            catch
            {
                TempData["message"] = "Unable to send request";
            }
            return View();
        }
        public ViewResult ViewOthersProfile(int id)
        {
            var profId = (from u in db.Users
                          where u.userid == id
                          select u.profileId).Single();
            Profile profile = db.Profiles.Single(p => p.profileId == profId);
            GlobalVariables.ProfileIdForCircle = Convert.ToInt32(profId);
            return View(profile);
        }
       
    }
}
