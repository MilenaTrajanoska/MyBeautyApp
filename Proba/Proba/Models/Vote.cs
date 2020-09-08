using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proba.Models
{
    public class Vote
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Salon")]
        public string SalonId { get; set; }
        public virtual Salon Salon { get; set; }
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int vote { get; set; }
    }
}