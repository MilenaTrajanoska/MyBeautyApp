using System;
using System.Collections.Generic;
using System.Configuration;
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
        [Authorize]
        public ActionResult Details(string id)
        {
            if (User.Identity.GetUserId() == id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (db.Salons.Find(id) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var salon = db.Salons.Include(s => s.Services).Where(s => s.UserId == id).Include(s => s.User).First();
                Vote vote = null;
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    vote = db.Votes.Where(v => v.ClientId == userId && v.SalonId == id).FirstOrDefault();
                }

                if (vote != null)
                {
                    ViewBag.votes = vote.vote;
                    ViewBag.salon = salon.UserId;
                }
                else
                {
                    ViewBag.votes = 0;
                }
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
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
             

                return RedirectToAction("AddServices","Salons", new{id = salon.UserId});
        }
        [Authorize]
        public ActionResult AddServices(string id)
        {
            if (User.Identity.GetUserId() == id)
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
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (User.Identity.GetUserId() == id)
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
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Salons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Edit([Bind(Include = "UserId,Name,Address,City,ImagePath,StartTime,EndTime,Rating")] Salon model)
        {
            if (ModelState.IsValid)
            {
                var newContext = new ApplicationDbContext();
                var postedFile = Request.Files["ImageFile"];
                model.ImageFile = postedFile.FileName == "" ? null : postedFile;
                if (model.ImageFile != null)
                {
                    String FileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);

                    //To Get File Extension  
                    string FileExtension = Path.GetExtension(model.ImageFile.FileName);

                    //Add Current Date To Attached File Name  
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                    //Get Upload path from Web.Config file AppSettings.  
                    
                    var ServerSavePath = Path.Combine(Server.MapPath("~/UserImages/") + FileName);
                    //Its Create complete path to store in server.  
                    model.ImagePath = ServerSavePath;

                    // To copy and save file into server.  
                    model.ImageFile.SaveAs(model.ImagePath);
                    model.ImagePath = FileName;

                }
                else
                {
                    model.ImagePath = db.Salons.Find(model.UserId).ImagePath;
                }

                newContext.Entry(model).State = EntityState.Modified;

                newContext.SaveChanges();
                return RedirectToAction("Details", new { id = model.UserId });
            }
            ViewBag.UserId = new SelectList(db.Salons, "Id", "Name", model.UserId);
            return View(model);
        }

        // GET: Salons/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (User.Identity.GetUserId() == id)
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
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Salons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            if (User.Identity.GetUserId() == id)
            {
                Salon salon = db.Salons.Find(id);
                db.Salons.Remove(salon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // rate\
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult PostRating(int rating, string salonId)
        {
            var Vote = new Vote
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = User.Identity.GetUserId(),
                SalonId = salonId,
                vote = rating
            };
            var salon = db.Salons.Find(salonId);
            salon.addVote(rating);
            //save data into the database
            Console.WriteLine(rating);
            db.Votes.Add(Vote);
            db.SaveChanges();
             //save into the database 
           // db.Salons.Find()
           // db.SaveChanges();
            return Json("Го оценивте салонот со " + rating.ToString() + " ѕвезди.");
        }
    }
}
