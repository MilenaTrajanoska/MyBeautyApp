﻿using Newtonsoft.Json;
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
        [Display(Name = "Слика")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile;

        [Display(Name = "Оцена")]
        public float Rating { get; set; }
        public float RatePoints { get; set; }

        public int NumReviews { get; set; }

        public DateTime DataNaKreiranje { get; set; }

        public Dictionary<string,int> VotersMap { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name="Почеток на работно време")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        [Display(Name="Крај на работно време")]
        [DataType(DataType.Time)]      
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }

        public float getRating()
        {
            return Rating;
        }

        public void addVote(string voter,int n)
        {
            if (VotersMap.ContainsKey(voter))
            {
                return;
            }
            VotersMap.Add(voter,n);
            RatePoints += n;
            ++NumReviews;
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
            
            
        }
    }
   
}