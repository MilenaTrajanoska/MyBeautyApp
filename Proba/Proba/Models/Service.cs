﻿using System;
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
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Име на сервис")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Tип на сервис")]
        public Type TypeOfService { get; set; }
        [Display(Name="Цена на услуга")]
        public int Price { get; set; }
        public Salon Salon { get; set; }
        [ForeignKey("Salon")]
        public string SalonId { get; set; }
    }
}