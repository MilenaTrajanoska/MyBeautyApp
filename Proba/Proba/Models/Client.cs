using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proba.Models
{
    public enum Gender
    {
        MALE,
        FEMALE,
    }

    public class Client
    {

        
            
            [Required]
            [Display(Name = "Име")]
            public string ClientName { get; set; }
            [Required]
            [Display(Name = "Презиме")]
            public string ClientSurname { get; set; }
            [Required]
            [Display(Name = "Пол")]
            public Gender Gender { get; set; }
            [Required]
            [Display(Name = "Датум на раѓање")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime DateOfBirth { get; set; }
            [Required]
            [Display(Name = "Град")]
            public string City { get; set; }
            [Display(Name = "Слика")]

            public virtual ApplicationUser User { get; set; }
        [Key]
        [ForeignKey("User")]

        public string UserId { get; set; }
        public Client()
           {

           }
    }
}