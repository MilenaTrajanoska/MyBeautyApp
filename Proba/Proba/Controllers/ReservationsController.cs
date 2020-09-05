using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Proba.Models;
using Syncfusion.JavaScript.Mobile;

namespace Proba.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index(string Id)
        {
            List<Reservation> reservations = new List<Reservation>();
            if (User.IsInRole("Salon"))
            {
               reservations = db.Reservations.Where(r => r.SalonId == Id).ToList();
            }
            if (User.IsInRole("Client"))
            {
                reservations = db.Reservations.Where(r => r.ClientId == Id).ToList();
            }
            
            return View(reservations);
        }

        // GET: Reservations/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reservations = db.Reservations
                                            .Include(m => m.Client)
                                            .Include(m=>m.Salon)
                                            .Include(m=>m.Service);
            Reservation reservation = reservations.Where(r => r.Id == id).First();

            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,SalonId,StartTime,EndTime,Notes")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,SalonId,StartTime,EndTime,Notes")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        /*
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }
        */
        // POST: Reservations/Delete/5       
        public ActionResult Delete(string id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            /*
            MailMessage mail = new MailMessage();
            mail.To.Add(salonEmail);
            mail.From = new MailAddress("trajanoska28@gmail.com");
            mail.Subject = "Откажана резервација";
            var clientName = db.Clients.Find(clientId).ClientName + " " + db.Clients.Find(clientId).ClientSurname;
            string Body = "<h4>Oткажана резервација</h4><br/>" +
                "Клиентот: " + clientName + "ја откажа резервацијата за датум: " + date.ToString("dd.mm.yyyy") 
                + "за услугата " + serviceName;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("my.beauty.app@outlook.com", "MyBeautyApp123!"); // Enter seders User name and password  
            smtp.Send(mail);
            */
            return RedirectToAction("Index");
        }


        public ActionResult GetReservationData()
        {
            var reservations = db.Reservations.ToList();
            return new JsonResult { Data = reservations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        
        public ActionResult MakeReservation(DetailsReservationViewModel model)
        {
            model.Salon = db.Salons.Find(model.SalonId);
            model.Service = db.Services.Find(model.ServiceId);
            model.ServiceId = model.ServiceId;
            

            return View(model);
        }
        public ActionResult ReservationTime(DetailsReservationViewModel pass)
        {
                   
            var email = User.Identity.Name;
            var user = db.Users.Where(u => u.Email == email).FirstOrDefault();
            var client = db.Clients.Find(user.Id);
            var service = db.Services.Find(pass.ServiceId);
            var salon = db.Salons.Find(pass.SalonId);
            var reservations = db.Reservations.Where(r => (r.SalonId == pass.SalonId && r.ServiceId == pass.ServiceId && r.Date == pass.Date)).ToList();
            DetailsReservationViewModel model = new DetailsReservationViewModel()
            {
                SalonId = pass.SalonId,
                ClientId = client.UserId,
                Date = pass.Date,
                Service = service,
                Salon = salon,
                ServiceId = service.Id,
                
            };

          
            if (reservations != null)
            {
                ViewBag.AllReservations = reservations;
            }
            else
            {
                ViewBag.AllReservations = new List<Reservation>();
            }
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult CreateReservation(DetailsReservationViewModel model)
        {
                         
            if (ModelState.IsValid)
            {
    
                var salon = db.Salons.Find(model.SalonId);

                var service = db.Services.Find(model.ServiceId);
                var client = db.Clients.Find(model.ClientId);
                var reservation = new Reservation() {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = model.ClientId,
                    Service = service,
                    Salon = salon,
                    Client = client,
                    SalonId = model.SalonId,
                    ServiceId = model.ServiceId,
                    Date = model.Date,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Notes = "Успешна резервација"
                };
                
                db.Reservations.Add(reservation);
                db.SaveChanges();

                return RedirectToAction("Details", "Reservations", new { id = reservation.Id });

            }
            else
            { 
                return RedirectToAction("Details", "Salons", new { id = model.SalonId });

            }

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
