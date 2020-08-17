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
        [Required]
        [Display(Name = "Слика")]
        public string ImagePath { get; set; }
        
        [Display(Name = "Оцена")]
        public float Rating { get; set; }
        public int NumReviews { get; set; }

        public Dictionary<string,int> VotersMap { get; set; }
        
  
        public float getRating()
        {
            return Rating / NumReviews;
        }

        public void addVote(string voter,int n)
        {
            if (VotersMap.ContainsKey(voter))
            {
                return;
            }
            VotersMap.Add(voter,n);
            Rating += n;
            ++NumReviews;
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
        }
    }
   
}