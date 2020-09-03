using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Proba.Models;

namespace Proba.Controllers
{
    public class SalonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<Service> services = new List<Service>()
            {
                new Service()
                {
                    TypeOfService = Models.Type.КОСА,
                    Name = Models.Type.КОСА.ToString(),
                },
                new Service()
                {
                    TypeOfService = Models.Type.ДЕПИЛАЦИЈА,
                    Name = Models.Type.ДЕПИЛАЦИЈА.ToString(),
                },
                new Service()
                {
                    TypeOfService = Models.Type.ПЕДИКИР,
                    Name = Models.Type.ПЕДИКИР.ToString(),
                },
                new Service()
                {
                    TypeOfService = Models.Type.МАНИКИР,
                    Name = Models.Type.МАНИКИР.ToString(),
                },
                new Service()
                {
                    TypeOfService = Models.Type.ШМИНКА,
                    Name = Models.Type.ШМИНКА.ToString(),
                },
            };

        // GET: Salons
        public ActionResult Index()
        {
            var salons = db.Salons.Include(s => s.User);
            return View(salons.ToList());
        }

        // GET: Salons/Details/5
        // Vidi detali za sekoj salon - passing id?

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var salon = db.Salons.Include(s => s.Services).Where(s => s.UserId == id).First();

            var model = new DetailsReservationViewModel()
            {
                Salon = salon,
                SalonId = salon.UserId,
            };
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(model);
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
        
        
        public ActionResult Create([Bind(Include = "UserId, User,Name,Address,City,Services,ImagePath,StartTime,EndTime")] Salon salon)
        {
                db.Salons.Add(salon);
                db.SaveChanges();
               
                foreach (var service in (List<Service>)TempData["Services"])

                {
                    db.Services.Add(service);
                    db.SaveChanges();
                }

                return RedirectToAction("AddServices","Salons", new{id = salon.UserId});
        }
        
        public ActionResult AddServices(string id)
        {
            //ViewBag.SalonId = id;
            var addServicesModel = new AddServicesViewModel()
            {
                SalonId = id,
                Service = new Service()
            };
            ViewBag.Services = services;
           
            return View(addServicesModel);
        }

        [HttpPost]
        
        public ActionResult AddServices(AddServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inputFiles = new List<string>();
                foreach (HttpPostedFileBase file in model.Service.files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/" ) + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user. 
                        /*
                            NE SE ZACHUVUVAAT PATEKITE VO BAZA!!!
                         
                         */

                        inputFiles.Add(InputFileName);
                       // ViewBag.UploadStatus = model.Service.files.Count().ToString() + " files uploaded successfully.";
                    }

                }

                var salon_db = db.Salons.Find(model.SalonId);
                salon_db.Services.Add(model.Service);
                db.Services.Add(model.Service);
                var service = db.Services.Find(model.Service.Id);
                foreach(var path in inputFiles)
                {
                   
                    service.StringsAsStrings = service.StringsAsStrings + path + ",";

                }
                
                db.SaveChanges();

                return RedirectToAction("AddServices");
            }

           // ViewBag.SalonId = m;
            ViewBag.Services = services;
            return View(model);
            
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
