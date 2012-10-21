using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeTube.Models;
using System.Net.Mail;

namespace LifeTube.Controllers
{ 
    public class UsersController : Controller
    {
        private LifeTubeEntities1 db = new LifeTubeEntities1();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            var users = db.Users.Include("Profile");
            return View(users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            User user = db.Users.Single(u => u.userid == id);
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
           // ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName");

            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            //if (ModelState.IsValid)
            //{
              
            //      User usr = new Models.User();

            //      usr.email = email;
            //      usr.password = model.Password;
            //      usr.firstName = model.firstName;

            //      var email = model.Email;
            //      var password = model.Password;
            //      User newUser = new Models.User();
                  
            //      newUser.email = email;
            //      //newUser.email = email;
            //      newUser.password = password;

            //      DB_46167_lifetubeEntities register = new DB_46167_lifetubeEntities();
             
                 
            //     register.Users.Add(newUser);
            //      register.SaveChanges();

           // }

            //ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName", user.profileid);
            //MailMessage message = new MailMessage();
            //message.From = new MailAddress("sarmin@mtxbd.com.com");
            //message.To.Add(new MailAddress(user.email));
            //message.Subject = " Welcome to LifeTube";
            //message.Body = "  Dear " + user.firstName + " " + user.lastName + "," + Environment.NewLine + "Thank you for joining the Matrix family." + Environment.NewLine + "We encourage you to collaborate with your GM and DGM for continuous success." + Environment.NewLine + "Username:" + user.firstName + " " + user.lastName + " " + Environment.NewLine + "Regards," + Environment.NewLine + "Matrix Admin";

            //SmtpClient client = new SmtpClient();
            //client.Send(message);
            return View();
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
            User user = db.Users.Single(u => u.userid == id);
            ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName", user.profileId);
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(user);
                db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName", user.profileId);
            return View(user);
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(int id)
        {
            User user = db.Users.Single(u => u.userid == id);
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            User user = db.Users.Single(u => u.userid == id);
            db.Users.DeleteObject(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}