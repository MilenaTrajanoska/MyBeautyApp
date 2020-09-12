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
using Microsoft.AspNet.Identity;
using Proba.Models;

namespace Proba.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.User);
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        // Vidi detali za sekoj korisnik- passing id?
        [Authorize]
        public ActionResult Details(string id)
        {
            if (User.Identity.GetUserId() == id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = db.Clients.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }
            return new HttpNotFoundResult();
        }

        // GET: Clients/Create
        /*public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Clients, "Id", "Name");
            return View();
        }*/

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
       public ActionResult Create([Bind(Include = "UserId,ClientName,ClientSurname,Gender,DateOfBirth,City, ImagePath")] Client client)
        {
            if (ModelState.IsValid)
            {   
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserId = new SelectList(db.Clients, "Id", "Name", client.UserId);
            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (User.Identity.GetUserId() == id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = db.Clients.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserId = new SelectList(db.Clients, "Id", "Name", client.UserId);
                return View(client);
            }
            return new HttpNotFoundResult();
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public ActionResult Edit([Bind(Include = "UserId,ImageFile,ClientName,ClientSurname,Gender,DateOfBirth,City, ImagePath")] Client model)
        {
            if (User.Identity.GetUserId() == model.UserId)
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
                        model.ImagePath = db.Clients.Find(model.UserId).ImagePath;
                    }

                    newContext.Entry(model).State = EntityState.Modified;

                    newContext.SaveChanges();
                    return RedirectToAction("Details", new { id = model.UserId });
                }
                ViewBag.UserId = new SelectList(db.Clients, "Id", "Name", model.UserId);
                return View(model);
            }
            return new HttpNotFoundResult();
        }

        // GET: Clients/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
