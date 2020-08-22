using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proba.Models
{
    public class DetailsReservationViewModel
    {
        public string SalonId { get; set; }
        public virtual Salon Salon { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Датум на резервација")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
       
        public string Notes { get; set; }
    }
}