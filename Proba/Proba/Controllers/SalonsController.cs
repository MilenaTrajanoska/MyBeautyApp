using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Proba.Models;

namespace Proba.Controllers
{
    public class SalonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Salons
        public ActionResult Index()
        {
            var salons = db.Salons.Include(s => s.User);
            return View(salons.ToList());
        }

        // GET: Salons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // GET: Salons/Create
        /*
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }*/

        // POST: Salons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        
        public ActionResult Create([Bind(Include = "UserId, User,Name,Address,City,Services,ImagePath")] Salon salon)
        {
            //salon.User = (ApplicationUser)TempData["User"];
            //salon.Services = (List<Service>)TempData["Services"];
           // if (ModelState.IsValid)
           // {


                db.Salons.Add(salon);
                db.SaveChanges();

                foreach (var service in (List<Service>)TempData["Services"])

                {
                    db.Services.Add(service);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
          //  }
        
          // ViewBag.UserId = new SelectList(db.Salons, "Id", "Name", salon.UserId);
          // return View(salon);
        }

        // GET: Salons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Salons, "Id", "Name", salon.UserId);
            return View(salon);
        }

        // POST: Salons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Address,City,numChairs")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Salons, "Id", "Name", salon.UserId);
            return View(salon);
        }

        // GET: Salons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        // POST: Salons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Salon salon = db.Salons.Find(id);
            db.Salons.Remove(salon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
