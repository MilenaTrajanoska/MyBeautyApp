using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proba.Models
{
    public enum Type
    {
            КОСА,
            ШМИНКА,
            МАНИКИР,
            ПЕДИКИР,
            ДЕПИЛАЦИЈА

    }
    public class Service
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Име на сервис")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Tип на сервис")]
        public Type TypeOfService { get; set; }
        [Display(Name = "Цена на услуга")]
        [Range(1, 10000)]
        public int Price { get; set; }
        public virtual Salon Salon {get; set;}
        [ForeignKey("Salon")]
        public string UserId { get; set; }

        [Display(Name ="Галерија")]
        [NotMapped]
        public List<HttpPostedFileBase> files { get; set; }

        
        
        public String StringsAsStrings { get; set; }

       

        public Service()
        {
            files = new List<HttpPostedFileBase>();
            StringsAsStrings = "";
            
        }



    }
}