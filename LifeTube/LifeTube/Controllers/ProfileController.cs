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
        private DB_46167_lifetubeEntities db = new DB_46167_lifetubeEntities();

        //
        // GET: /Profile/

        public ViewResult Profile()
        {
            return View(db.Profiles.ToList());
        }

        //
        // GET: /Profile/Details/5

        public ViewResult Details(int id)
        {
            Profile profile = db.Profiles.Single(p => p.profileid == id);
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
        public ActionResult Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.AddObject(profile);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(profile);
        }
        
        //
        // GET: /Profile/Edit/5
 
        public ActionResult Edit(int id)
        {
            Profile profile = db.Profiles.Single(p => p.profileid == id);
            return View(profile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Attach(profile);
                db.ObjectStateManager.ChangeObjectState(profile, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        //
        // GET: /Profile/Delete/5
 
        public ActionResult Delete(int id)
        {
            Profile profile = db.Profiles.Single(p => p.profileid == id);
            return View(profile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Profile profile = db.Profiles.Single(p => p.profileid == id);
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