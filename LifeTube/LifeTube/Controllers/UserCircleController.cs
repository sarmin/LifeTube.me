using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LifeTube.Models;
using System.Collections;

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
            
            return View(myCircle);

            //var myCircle = (from usr in db.Users
            //                from uc in db.UserCircles
            //                where uc.userFrom == GlobalVariables.UserId || uc.userTo == GlobalVariables.UserId
            //                where uc.isAccepted == true
            //                where usr.userid == uc.userFrom /*|| usr.userid == uc.userTo*/
            //                select new UserCircleModel()
            //                {                             

            //                    firstName=usr.firstName,
            //                    relationship=uc.relationship, 

            //                }).ToList();



            //var myCircle = (from uc in db.UserCircles
            //                where uc.userFrom == GlobalVariables.UserId || uc.userTo == GlobalVariables.UserId
            //                select uc).ToArray();

            //var uFrom = (from f in myCircle select f.userFrom).ToList();
            //var uTo = (from t in myCircle select t.userTo).ToList();

            //UserCircleModel model = new UserCircleModel();

            //for (int i = 0; i < myCircle.Length; i++)
            //{
            //    if (uFrom[i] == GlobalVariables.UserId)
            //    {
            //        var name = (from usr in db.Users
            //                    where usr.userid == uTo[i]
            //                    select usr.firstName);

            //        model.firstName = name.ToString();

            //    }
            //    else if (uTo[i] == GlobalVariables.UserId)
            //    {
            //        var name = (from usr in db.Users
            //                    where usr.userid == uFrom[i]
            //                    select usr.firstName);

            //        model.firstName = name.ToString();
            //    }
            //    else
            //    {
            //    }
            //}
            //return View(myCircle);
           
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
            //return Json(results);
            return Json(results, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetUsers()
        {
            var allUsrs = (from u in db.Users
                           from uc in db.UserCircles
                           where u.userid != GlobalVariables.UserId
                           // where uc.userFrom!=u.userid || uc.userTo!=u.userid

                           select u.firstName);
            return View(allUsrs);
        }
        public ActionResult AddToCircle(AddToCircleModel model)
        {
            UserCircle uc = new UserCircle();
            uc.userFrom = GlobalVariables.UserId;
            uc.createdOn = DateTime.Now;
            uc.lastModifiedDate = DateTime.Now;
            uc.lastModifiedBy = GlobalVariables.UserId;
            uc.userTo = model.userTo;
            uc.isAccepted = false;

            db.UserCircles.AddObject(uc);
            db.SaveChanges();

            TempData["message"] = "Add request has been sent";
            return View();
        }
       
       
    }
}
