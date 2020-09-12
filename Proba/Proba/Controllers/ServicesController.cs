using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proba.Models;
using Type = Proba.Models.Type;

namespace Proba.Controllers
{
    public class ServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Services
        public ActionResult Index(string id)
        {
            var services = db.Services.Include(s => s.Salon).Where(s => s.Salon.UserId == id).ToList();
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Salons, "UserId", "Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TypeOfService,Price,UserId")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Salons, "UserId", "Name", service.UserId);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Salons, "UserId", "Name", service.UserId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Duration,TypeOfService,Price,UserId,StringsAsStrings,files")] Service model)
        {
            if (ModelState.IsValid)
            {
                var files = Request.Files.GetMultiple("files");
                
                if (files != null)
                {
                    var inputFiles = new List<string>();
                    foreach (HttpPostedFileBase file in files)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                            //Save file to server folder  
                            file.SaveAs(ServerSavePath);
                            //assigning file uploaded status to ViewBag for showing message to user. 
                            inputFiles.Add(InputFileName);
                            // ViewBag.UploadStatus = model.Service.files.Count().ToString() + " files uploaded successfully.";
                        }

                    }

                    var service = db.Services.Find(model.Id);
                    foreach (var path in inputFiles)
                    {

                        service.StringsAsStrings = service.StringsAsStrings + path + ",";

                    }
                }
                TryUpdateModel(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Services", new { id = model.UserId });
            }
            return View(model);
        }

        // GET: Services/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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


        public ActionResult ServiceK(Type type)
        {
            ViewBag.Message = "УСЛУГА " + type;
            var saloni = getSaloniSoUsluga(type);
            return View(saloni);
        }
        public ActionResult ServicePARTIAL(Type type)
        {
            ViewBag.Message = "УСЛУГА " + type;
            ViewBag.Id = "table" + type.ToString();
            var saloni =  getSaloniSoUsluga(type);
            return PartialView(saloni);
        }


        public List<Salon> getSaloniSoUsluga(Type usluga)
        {
            var saloni = db.Salons.ToList().Where(s => s.ContainsService(usluga)).Select(iv => iv).ToList();

            //var saloni = db.Salons.Where(s => s.ContainsService(usluga)).ToList();
            return saloni;
        }
    }
}
