using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeTube.Models;

namespace LifeTube.Controllers
{ 
    public class UsersController : Controller
    {
        private DB_46167_lifetubeEntities db = new DB_46167_lifetubeEntities();

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
            ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName");

            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                DB_46167_lifetubeEntities lifetube = new DB_46167_lifetubeEntities();

                //User user = new User();

                //usr.email =
                db.Users.AddObject(user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName", user.profileid);
            return View(user);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
            User user = db.Users.Single(u => u.userid == id);
            ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName", user.profileid);
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
            ViewBag.profileid = new SelectList(db.Profiles, "profileid", "firstName", user.profileid);
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