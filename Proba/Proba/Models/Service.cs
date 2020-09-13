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
        [Required(ErrorMessage = "Задолжително внесете име на сервис")]
        [Display(Name = "Име на сервис")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Задолжително внесете тип на сервис")]
        [Display(Name = "Tип на сервис")]
        public Type TypeOfService { get; set; }
        [Display(Name = "Цена на услуга")]
        [Required(ErrorMessage = "Задолжително внесете цена на услугата")]
        [Range(1, 10000,ErrorMessage = "Цената на услугата треба да биде помеѓу 1 и 10000 денари")]
        public int Price { get; set; }
        public virtual Salon Salon {get; set;}
        [ForeignKey("Salon")]
        public string UserId { get; set; }

        [Display(Name ="Галерија")]
        [NotMapped]
        public List<HttpPostedFileBase> files { get; set; }

        public string StringsAsStrings { get; set; }
        [Required(ErrorMessage = "Задолжително внесете времетраење на услуга")]
        [Display(Name="Времетраење на услуга во минути")]
        [Range(1, 180, ErrorMessage ="Невалидна вредност за времетраење на услуга")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public int Duration { get; set; }


        public Service()
        {
            files = new List<HttpPostedFileBase>();
            StringsAsStrings = "";
        }



    }
}