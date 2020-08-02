using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proba.Models
{
    public class Salon
    {
        [Required]
        [Display(Name="Име")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Адреса")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Град")]
        public string City { get; set; }
        [Display(Name="Број на вработени")]
        [Range(0,1000)]
        public int numChairs { get; set; }

        public virtual ApplicationUser User { get; set; }
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Display(Name="Услуги")]
        public virtual List<Service> Services { get; set; }
        public Salon()
        {
            Services = new List<Service>();
            numChairs = 0;
        }
    }
   
}