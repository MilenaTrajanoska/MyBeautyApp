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
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        [Display(Name ="Име на клиент")]
        public virtual Client Client { get; set; }
        [Display(Name ="Име на салон")]
        public virtual Salon Salon { get; set; }
        [ForeignKey("Salon")]
        public string SalonId { get; set; }
        [ForeignKey("Service")]
        public int  ServiceId { get; set; }
        [Display(Name ="Име на сервис")]
        public virtual Service Service { get; set; }
        [Display(Name ="Почеток на термин")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Крај на термин")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime EndTime { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Датум на резервација")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Забелешки")]
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