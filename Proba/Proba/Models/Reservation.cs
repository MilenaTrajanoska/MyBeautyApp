using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proba.Models
{
    public class Reservation
    {
        [Key]
        public string Id { get; set; }
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Salon Salon { get; set; }
        public string SalonId { get; set; }
        public Service Service { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Reservation objAsReservation = obj as Reservation;
            if (objAsReservation == null) return false;
            else return objAsReservation.EndTime == this.EndTime && objAsReservation.StartTime == this.StartTime;
        }


    }
}