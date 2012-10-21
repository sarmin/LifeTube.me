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
    public class ProfileController : Controller
    {
        private LifeTubeEntities1 db = new LifeTubeEntities1();
        //
        // GET: /Profile/

        public ViewResult Profile()
        {
            //return View(db.Profiles.ToList());
            var prfl=(from p in db.Profiles
                          from u in db.Users
                          where u.userid==GlobalVariables.UserId
                          where u.profileId==p.profileId
                          select p).ToArray();
            
            return View(prfl);
                      
        }

        //
        // GET: /Profile/Details/5

        public ViewResult Details(int id)
        {
            Profile profile = db.Profiles.Single(p => p.profileId == id);
            return View(profile);
        }

        //
        // GET: /Profile/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Profile/Create

        [HttpPost]
        public ActionResult CreateProfile(CreateProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                //db.Profiles.AddObject(profile);
                //db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(profile);
        }
        
        //
        // GET: /Profile/Edit/5

        public ActionResult EditProfile(int id)
        {
            var profileDetail = (from p in db.Profiles
                                 where p.profileId == id
                                 select p).ToArray();
            return View();
          
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult EditProfile(Profile profile)
        {

            if (ModelState.IsValid)
            {
                db.Profiles.Attach(profile);
                db.ObjectStateManager.ChangeObjectState(profile, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Profile", "Profile");
            }
            return View("profile");

        }

        //
        // GET: /Profile/Delete/5
 
        public ActionResult Delete(int id)
        {
            Profile profile = db.Profiles.Single(p => p.profileId == id);
            return View(profile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Profile profile = db.Profiles.Single(p => p.profileId == id);
            db.Profiles.DeleteObject(profile);
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