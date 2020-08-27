using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Index()
        {
            return View();
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

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GetReservationData()
        {
            var reservations = db.Reservations.ToList();
            return new JsonResult { Data = reservations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /*public ActionResult GetReservationDetailById(string reservationId)
        {
            var reservationDetail = db.Reservations.ToList().Where(x => x.Id == reservationId);
            return new JsonResult { Data = reservationDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public void UpdateReservationDetails(string reservationId, DateTime startDate, DateTime endDate)
        {
            var reservationDetail = db.Reservations.ToList().Where(x => x.Id == reservationId).First();
            reservationDetail.StartTime = startDate;
            reservationDetail.EndTime = endDate;
            db.SaveChanges();
        }*/
        
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

            /*var jsonStr = salon.ReservationsAsJson;
            var res = JsonConvert.DeserializeObject<Dictionary<DateTime, List<Reservation>>>(jsonStr);
            if (res.ContainsKey(pass.Date)){
                ViewBag.AllReservations = res[pass.Date];
            }
            else
            {
                ViewBag.AllReservations = new List<Reservation>();
            }
            */
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
                //var jsonStr = salon.ReservationsAsJson;
                //var reservations = JsonConvert.DeserializeObject<Dictionary<DateTime, List<Reservation>>>(jsonStr);
                //var reservations = db.Reservations.Where(r => (r.SalonId == model.SalonId && r.ServiceId == model.ServiceId && r.Date == model.Date));
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
                    Notes = "Uspeshna rezervacija"
                };
                /*
                
                if (!reservations.ContainsKey(model.Date))
                {
                    reservations[model.Date] = new List<Reservation>();
                    
                }

                reservations[model.Date].Add(reservation);
                var updateStr = JsonConvert.SerializeObject(reservations, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                //salon.ReservationsAsJson = updateStr;*/
                db.Reservations.Add(reservation);
                db.SaveChanges();
                ViewBag.SuccessMessage = "<span style='color:green'>Успешна резервација!</span>";

                return RedirectToAction("Details", "Reservations", new { id = reservation.Id });

            }
            else
            {
                ViewBag.SuccessMessage = "<span style='color:red'>Неуспешна резервација!</span>";
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
