using Newtonsoft.Json;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.Shared.Serializer;
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
        [Required(ErrorMessage ="Задолжително внесете име")]
        [Display(Name="Име")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Задолжително внесете адреса")]
        [Display(Name="Адреса")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Задолжително внесете град")]
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
        [Display(Name = "Слика")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile;

        [Display(Name = "Оцена")]
        public float Rating { get; set; }
        public float RatePoints { get; set; }

        public int NumReviews { get; set; }

        public DateTime DataNaKreiranje { get; set; }

        public Dictionary<string,int> VotersMap { get; set; }
        public virtual List<Vote> votes { get; set; }

        [Required(ErrorMessage = "Задолжително внесете почеток на работното време")]
        [DataType(DataType.Time)]
        [Display(Name="Почеток на работно време")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        [Display(Name="Крај на работно време")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Задолжително внесете крај на работното време")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }

        public float getRating()
        {
            return Rating;
        }

        public void addVote(int n)
        {
            RatePoints += n;
            NumReviews++;
            Rating = RatePoints / NumReviews;
        }

        public bool ContainsService(Type s)
        {
            foreach(Service service in Services)
            {
                if (service.TypeOfService == s)
                {
                    return true;
                }
            }
            return false;
        }


        public Salon()
        {
            Services = new List<Service>();
            VotersMap = new Dictionary<string,int>();
            numChairs = 0;
            NumReviews = 0;
            Rating = 0;
            DataNaKreiranje = DateTime.Now;
            votes = new List<Vote>();            
        }
    }
   
}