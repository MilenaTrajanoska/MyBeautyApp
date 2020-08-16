using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Proba.Models;

namespace Proba.Controllers
{
    public class SalonsControllerAPI : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SalonsControllerAPI
        public IQueryable<Salon> GetSalons()
        {
            return db.Salons;
        }

/*
        public ActionResult searchTown(string town)
        {
            var salons = GetSalons(town);
            return PartialView(salons);
        }
        
        private List<Salon> GetSalons(string town)
        {
            return db.Salons.Where(s => s.City.Contains(town)).ToList();
        }
*/

        // GET: api/SalonsControllerAPI/5
        [ResponseType(typeof(Salon))]
        public IHttpActionResult GetSalon(string id)
        {
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return NotFound();
            }

            return Ok(salon);
        }

        // PUT: api/SalonsControllerAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalon(string id, Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salon.UserId)
            {
                return BadRequest();
            }

            db.Entry(salon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SalonsControllerAPI
        [ResponseType(typeof(Salon))]
        public IHttpActionResult PostSalon(Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Salons.Add(salon);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalonExists(salon.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salon.UserId }, salon);
        }

        // DELETE: api/SalonsControllerAPI/5
        [ResponseType(typeof(Salon))]
        public IHttpActionResult DeleteSalon(string id)
        {
            Salon salon = db.Salons.Find(id);
            if (salon == null)
            {
                return NotFound();
            }

            db.Salons.Remove(salon);
            db.SaveChanges();

            return Ok(salon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalonExists(string id)
        {
            return db.Salons.Count(e => e.UserId == id) > 0;
        }
    }
}