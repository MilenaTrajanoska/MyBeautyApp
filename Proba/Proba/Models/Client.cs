﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proba.Models
{
    public enum Gender
    {
        МАШКИ,
        ЖЕНСКИ,
    }

    public class Client
    {
        [Required(ErrorMessage = "Задолжително внесете име")]
        [Display(Name = "Име")]
            public string ClientName { get; set; }
        [Required(ErrorMessage = "Задолжително внесете презиме")]
        [Display(Name = "Презиме")]
            public string ClientSurname { get; set; }
        [Required(ErrorMessage = "Задолжително внесете пол")]
        [Display(Name = "Пол")]
            public Gender Gender { get; set; }

        [Required(ErrorMessage = "Задолжително внесете датум на раѓање")]
        [Display(Name = "Датум на раѓање")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Задолжително внесете град")]
        [Display(Name = "Град")]
            public string City { get; set; }
            

            public virtual ApplicationUser User { get; set; }
        [Key]
        [ForeignKey("User")]

        public string UserId { get; set; }
        
        [Display(Name = "Слика")]
        [AllowHtml]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile;
        public Dictionary<DateTime, List<Reservation>> Reservations { get; set; }
        public virtual List<Vote> votes { get; set; }
        public Client()
           {
            Reservations = new Dictionary<DateTime, List<Reservation>>();
            votes = new List<Vote>();
           }
    }
}