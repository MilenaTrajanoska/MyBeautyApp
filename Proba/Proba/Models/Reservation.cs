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
        public string SalonId { get; set; }
        public Service Service { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }


    }
}